using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public class ScreenGame : Screen {

    public int numOfRounds;
    public static int roundsLeft;
    public bool didGameEnd = false;
    public bool didRoundEnd = false;

    Timer nextGameTimer;
    Timer gameWinnerTimer;
    Timer weaponSpawnTimer;

    [HideInInspector]
    public List<GameObject> Characters = new List<GameObject>();
    LevelMap levelMap;
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> playerSpawns = new List<GameObject>();
    List<GameObject> weaponSpawns = new List<GameObject>();
    GameObject camera;

    GameObject scoreCanvasObj;


    public ScreenGame(int _numOfRounds)
    {
        screenType = ScreenType.GAME;
        numOfRounds = _numOfRounds;
        roundsLeft = _numOfRounds;
        didGameEnd = false;

        nextGameTimer = new Timer(5);
        gameWinnerTimer = new Timer(5);
        weaponSpawnTimer = new Timer(1);
        InitNewGame();
    }


    public override void DestroyNonPassableResources()
    {
    }
    public override void DestroyPassableResources()
    {
    }
    public override void UpdateScreen()
    {

        weaponSpawnTimer.Tick(Time.deltaTime);
        if (weaponSpawnTimer.IsFinished()) {
            Debug.Log(weaponSpawns.Count);
            SpawnWeaponbox();
            weaponSpawnTimer.Reset();
        }

        if (DidRoundEnd() && !didGameEnd) {
            nextGameTimer.Tick(Time.deltaTime);
            //if canvas is not yet on screen, keep zooming to winner
            if(nextGameTimer.GetTimePassed() < 2) {
                CameraScript.ZoomIn(3);
            }
            //if score canvas is not created(not null), destroy level and create it
            if (!IsScoreCanvasOnScreen() && nextGameTimer.GetTimePassed() > 2) { 
                DestroyLevel();
                CreateScoreCanvas();
            }
            //if timer is done spawn next game
            if (nextGameTimer.IsFinished())
            {
                if (AreAnyRoundsLeft()) {
                    CameraScript.ResetZoom();
                    InitNewGame();
                }else{
                    didGameEnd = true;                    
                    //create winner screen/gui 
                }
            }
        }
        else if (didGameEnd) {
            gameWinnerTimer.Tick(Time.deltaTime);
            if (gameWinnerTimer.IsFinished()) {   
                CameraScript.ResetZoom();
                Profile.ClearProfileList();
                Screen.ChangeTo(ScreenType.CHARACTER_CHOICE);
            }
        }
        
        

    }

    void SpawnWeaponbox(){
        int wSpawnPoint = Random.Range(0, weaponSpawns.Count);
        for(int i = 0; i < weaponSpawns.Count; ++i) {
            if(wSpawnPoint == i) {
                GameObject wBoxObj = Resources.Load<GameObject>(FilePaths.objWeaponbox);
                wBoxObj.transform.position = weaponSpawns[i].transform.position;
                MonoBehaviour.Instantiate(wBoxObj);
            }

        }
    }

    //********************** START GAME ********************
    void InitNewGame()
    {
        nextGameTimer.Reset();

        //spawn map with already intantiated gameObjects
        levelMap = MapParser.GetRandomMap();
        ExtractSpecificFromAllGO();
        //chose where characters will spawn
        InitCharacters();
        SetCharactersPosition();
        //set camera position
        SetCameraPosition();
    }

    //Instantiate all character objects
    void InitCharacters()
    {
        foreach(Profile profile in Profile.s_profiles) {
            if (profile.isProfileReady){
                profile.CreateCharacterObject(new Vector2(0, 0)); 
                Characters.Add(profile.character);
                }
        }        
    }
    //remove character object
    public void RemoveCharacterFromList(ref GameObject objToRemove)
    {
        for(int i = Characters.Count-1; i >= 0; --i) {
            if(objToRemove == Characters[i])
             Characters.Remove(objToRemove);
        }
    }
    
    //************************SORT ALL OBJECT IN SOME CATEGORIES**************************
    void ExtractSpecificFromAllGO()
    {
         foreach(GameObject obj in levelMap.mapObjects) {
            Sprite objSprite = obj.GetComponent<SpriteRenderer>().sprite;
            //platforms.....
            if (ResourceReader.platformSpriteMap.ContainsKey(objSprite)) {
                obj.AddComponent<BoxCollider2D>();
                obj.layer = 20;
                platforms.Add(obj);
            }
            //palyer spawn
            else if (ResourceReader.itemSpriteMap.ContainsKey(objSprite))
            {
                if(objSprite.name == "cam") {
                    camera = obj;
                }
                else if(objSprite.name == "player_spawn") {
                    playerSpawns.Add(obj);
                }
                else if(objSprite.name == "weapon_spawn") {
                    weaponSpawns.Add(obj);
                }

            }

        }      
    }

    void SetCharactersPosition(){        
        foreach(GameObject player in Characters) {
            int rand = Random.Range(0, playerSpawns.Count);
            for (int i = 0; i < playerSpawns.Count; ++i) {
                Debug.Log("workzzz");
                if (rand == i)
                    player.transform.position = playerSpawns[i].transform.position;
            }
        }
    }

    void SetCameraPosition() {
        if (camera != null)
        {
            Camera.main.transform.position = camera.transform.position;            
            Debug.Log("FROM" + Camera.main.transform.position + " TO" + camera.transform.position);
        }
    }

    void DestroyLevel(){
        //destroy all bullets on screen
        for (int i = Bullet.bulletsOnScreen.Count-1; i >= 0; --i) {
            MonoBehaviour.Destroy(Bullet.bulletsOnScreen[i]);
        }
        Bullet.bulletsOnScreen.Clear();
        //Destroy map object
        for (int i = levelMap.mapObjects.Count-1; i >= 0; --i) {
            MonoBehaviour.Destroy(levelMap.mapObjects[i]);
        }        
        platforms.Clear();
        playerSpawns.Clear();
        weaponSpawns.Clear();
        //Destroy characters
        for (int i = Characters.Count-1; i >= 0; --i) {
            MonoBehaviour.Destroy(Characters[i]);
        } 
        Characters.Clear();
        //reduce number of rounds by 1
        roundsLeft--;
    }

    bool AreAnyRoundsLeft(){
        return roundsLeft > 0;
    }
    bool DidRoundEnd(){
        return Characters.Count <= 1;
    }

    void CreateScoreCanvas() {
        GameObject scoreCanvasPrefab = Resources.Load<GameObject>(FilePaths.canvasScore);
        scoreCanvasObj = MonoBehaviour.Instantiate(scoreCanvasPrefab);
    }
    bool IsScoreCanvasOnScreen() {
        return scoreCanvasObj != null;
    }



}
