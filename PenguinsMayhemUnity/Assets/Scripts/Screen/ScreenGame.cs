using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenGame : Screen {

    public int numOfRounds;
    public int roundsLeft;

    List<GameObject> Characters = new List<GameObject>();
    LevelMap levelMap;
    List<GameObject> platforms = new List<GameObject>();


    public ScreenGame(int _numOfRounds)
    {
        screenType = ScreenType.GAME;
        numOfRounds = _numOfRounds;
        roundsLeft = _numOfRounds;

        //spawn map with already intantiated gameObjects
        levelMap = MapParser.GetRandomMap();
        ExtractPlatformsFromAllGO();
        //chose where characters will spawn
        InitCharacters();
        
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
    
    //************************SET UP PLATFORMS**************************
    void ExtractPlatformsFromAllGO()
    {
         foreach(GameObject obj in levelMap.mapObjects) {
           // if (obj.GetComponent<SpriteRenderer>().sprite == null)
           //     continue;
            Sprite objSprite = obj.GetComponent<SpriteRenderer>().sprite;
            if (ResourceReader.platformSpriteMap.ContainsKey(objSprite)) {
                obj.AddComponent<BoxCollider2D>();
                platforms.Add(obj);
            }
        }      
    }



}
