using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

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
    List<GameObject> weaponBoxes = new List<GameObject>();
    GameObject camera;

    GameObject scoreCanvasObj;
    GameObject winnerCanvasObj;
    GameObject confettiSpawner;


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
            SpawnWeaponbox();
            weaponSpawnTimer.Reset();
        }

        //***IF ROUND ENDED AND GAME IS NOT DONE(still rounds left)***
        if (DidRoundEnd() && !didGameEnd) {
            nextGameTimer.Tick(Time.deltaTime);
            //if canvas is not yet on screen, keep zooming to winner
            if (nextGameTimer.GetTimePassed() < 2) {
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
                    InitNewGame();
                } else {
                    didGameEnd = true;
                    //create winner screen/gui 
                }
            }
        }
        //***IF GAME ENDED AND THERE IS A WINNER***
        else if (didGameEnd && IsThereAWinner()) {
            gameWinnerTimer.Tick(Time.deltaTime);
            if (!IsWinnerCanvasOnScreen()) {
                CreateAndInitWinnerCanvas();
            }
            if (gameWinnerTimer.IsFinished()) {
                MonoBehaviour.Destroy(winnerCanvasObj);
                confettiSpawner.GetComponent<confettiSpawn>().DestroySpawner();
                CameraScript.ResetZoom();
                Profile.ClearProfileList();
                Screen.ChangeTo(ScreenType.CHARACTER_CHOICE);
            }
        }
        //***IF GAME ENDED AND THERE IS NO WINNER***
        else if (didGameEnd && !IsThereAWinner()) {
            InitNewGame();
            didGameEnd = false;
        }

    }


    bool IsThereAWinner() {
        int highestScore = 0;
        //find biggest score
        foreach (Profile prof in Profile.s_profiles)
            if (prof.score > highestScore)
                highestScore = prof.score;
        //check if more then one have biggest score
        int counter = 0;
        foreach (Profile prof in Profile.s_profiles)
            if (prof.score == highestScore) {
                counter++;
            }
        //return if more then one profile holds same score
        if (counter > 1)
            return false;
        return true;
    }


    void SpawnWeaponbox() {
        int wSpawnPoint = Random.Range(0, weaponSpawns.Count);
        for (int i = 0; i < weaponSpawns.Count; ++i) {
            if (wSpawnPoint == i) {
                GameObject wBoxObj = Resources.Load<GameObject>(FilePaths.objWeaponbox);
                wBoxObj.transform.position = weaponSpawns[i].transform.position;
                weaponBoxes.Add(MonoBehaviour.Instantiate(wBoxObj));
            }

        }
    }

    //********************** START GAME ********************
    void InitNewGame()
    {
        CameraScript.ResetZoom();
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
        foreach (Profile profile in Profile.s_profiles) {
            if (profile.isProfileReady) {
                profile.CreateCharacterObject(new Vector2(0, 0));
                Characters.Add(profile.character);
            }
        }
    }
    //remove character object
    public void RemoveCharacterFromList(ref GameObject objToRemove)
    {
        for (int i = Characters.Count - 1; i >= 0; --i) {
            if (objToRemove == Characters[i])
                Characters.Remove(objToRemove);
        }
    }

    //************************SORT ALL OBJECT IN SOME CATEGORIES**************************
    void ExtractSpecificFromAllGO()
    {
        foreach (GameObject obj in levelMap.mapObjects) {
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
                if (objSprite.name == "cam") {
                    camera = obj;
                }
                else if (objSprite.name == "player_spawn") {
                    playerSpawns.Add(obj);
                }
                else if (objSprite.name == "weapon_spawn") {
                    weaponSpawns.Add(obj);
                }

            }

        }
    }

    void SetCharactersPosition() {
        foreach (GameObject player in Characters) {
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

    //*********Destroy object and clear list********************
    void DestroyAndClearList(ref List<GameObject> _list) {
        for (int i = _list.Count - 1; i >= 0; --i) {
            MonoBehaviour.Destroy(_list[i]);
        }
        _list.Clear();
    }

    void DestroyLevel() {
        DestroyAndClearList(ref Bullet.bulletsOnScreen);
        DestroyAndClearList(ref levelMap.mapObjects);
        platforms.Clear();
        playerSpawns.Clear();
        weaponSpawns.Clear();
        DestroyAndClearList(ref Characters);
        DestroyAndClearList(ref weaponBoxes);

        roundsLeft--;
    }

    //**** information about round ****
    bool AreAnyRoundsLeft() {
        return roundsLeft > 0;
    }
    bool DidRoundEnd() {
        return Characters.Count <= 1;
    }
    //***** score canvas ******
    void CreateScoreCanvas() {
        GameObject scoreCanvasPrefab = Resources.Load<GameObject>(FilePaths.canvasScore);
        scoreCanvasObj = MonoBehaviour.Instantiate(scoreCanvasPrefab);
    }
    bool IsScoreCanvasOnScreen() {
        return scoreCanvasObj != null;
    }
    //***** winner canvas ******
    void CreateAndInitWinnerCanvas() {
        GameObject winnerCanvasPrefab = Resources.Load<GameObject>(FilePaths.canvasWinner);
        winnerCanvasObj = MonoBehaviour.Instantiate(winnerCanvasPrefab);
        //background confetti
        GameObject confettiSpawnerPrefab = Resources.Load<GameObject>(FilePaths.objConfettiSpawner);
        confettiSpawner = MonoBehaviour.Instantiate(confettiSpawnerPrefab);
        //set canvas gui properties from profile
        Profile winnerProfile = GetWinnerProfile();
        winnerCanvasObj.transform.FindChild("Character").FindChild("Name").GetComponent<Text>().text = winnerProfile.name;
        winnerCanvasObj.transform.FindChild("Character").FindChild("ImageHat").GetComponent<Image>().sprite = winnerProfile.hatSprite;
        winnerCanvasObj.transform.FindChild("Character").FindChild("Score").GetComponent<Text>().text = winnerProfile.score.ToString();
    }
    bool IsWinnerCanvasOnScreen() {
        return winnerCanvasObj != null;
    }
    //********** Get winner profile
    Profile GetWinnerProfile() {
        Profile targetProfile = Profile.s_profiles[0];
        foreach (Profile prof in Profile.s_profiles) {
            if (prof.score > targetProfile.score)
                targetProfile = prof;
        }
        return targetProfile;
    }



}
