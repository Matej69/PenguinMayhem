using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ResourceReader {

    private static int spriteID = 0;
    
    //public static Sprite[] platformSprites = GetSprites(FilePaths.spritePlatform);
    //public static Sprite[] entitySprites = GetSprites(FilePaths.spriteEntity);

    public static Dictionary<int, Sprite> platformSpriteMap = GetSprites(FilePaths.spritePlatform);

    //public enum spriteID { GROUND_LEFT,GROUND,GROUND_RIGHT,SNOW_V,SNOW_V_TOP,SNOW_V_BOT};


/*
    public static GameObject[] GetGameObjects(string _path)
    {
        GameObject[] objects = Resources.LoadAll<GameObject>(_path);
        Debug.Log(objects.Length);
        return objects;
    }
*/
    public static Dictionary<int, Sprite> GetSprites(string _path)
    {
        Dictionary<int, Sprite> spriteMap = new Dictionary<int, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(_path);       
         
        foreach (Sprite _sprite in sprites)
        {
            spriteMap[spriteID] = _sprite;
            spriteID++;
        }        
        return spriteMap;
    }    


}
