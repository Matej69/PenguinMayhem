using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public class ScreenGame : Screen {

    public int numOfRounds;
    public int roundsLeft;

    List<GameObject> Characters = new List<GameObject>();
    LevelMap levelMap;
    List<GameObject> platforms = new List<GameObject>();
    List<GameObject> playerSpawns = new List<GameObject>();
    List<GameObject> weaponSpawns = new List<GameObject>();
    GameObject camera;


    public ScreenGame(int _numOfRounds)
    {
        screenType = ScreenType.GAME;
        numOfRounds = _numOfRounds;
        roundsLeft = _numOfRounds;

        //spawn map with already intantiated gameObjects
        levelMap = MapParser.GetRandomMap();
        ExtractSpecificFromAllGO();
        //chose where characters will spawn
        InitCharacters();
        SetCharactersPosition();
        //set camera position
        SetCameraPosition();
        
        
    }


    public override void DestroyNonPassableResources()
    {
    }
    public override void DestroyPassableResources()
    {
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
                    Debug.Log("camera is here dude");
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



}
