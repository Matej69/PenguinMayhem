using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ResourceReader {

    private static int spriteID = 0;

    //public static Sprite[] platformSprites = GetSprites(FilePaths.spritePlatform);
    //public static Sprite[] entitySprites = GetSprites(FilePaths.spriteEntity);

    //public static EntityInfo platformInfo = new EntityInfo()
    
    public static Dictionary<Sprite, int> platformSpriteMap = GetSprites(FilePaths.spritePlatform);
    public static Dictionary<Sprite, int> backgroundSpriteMap = GetSprites(FilePaths.spriteBackground);
    public static Dictionary<Sprite, int> itemSpriteMap = GetSprites(FilePaths.spriteItem);


    /*
    public static GameObject[] GetGameObjects(string _path)
    {
        GameObject[] objects = Resources.LoadAll<GameObject>(_path);
        Debug.Log(objects.Length);
        return objects;
    }
*/
    public static Dictionary<Sprite, int> GetSprites(string _path)
    {
        Dictionary<Sprite, int> spriteMap = new Dictionary<Sprite, int>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(_path);

         
        foreach (Sprite _sprite in sprites)
        {
            //if reading platform sprites
            //if(_path == FilePaths.spritePlatform) {
                spriteMap[_sprite] = spriteID;
           /* }
            //if reading backgorund sprites
            else if (_path == FilePaths.spriteBackground) {
                spriteMap[_sprite] = spriteID ;
            }
            //if reading item sprites
            else if (_path == FilePaths.spriteItem) {
                if(_sprite.name == FilePaths.spriteFilenamePlayerSpawn)
                    spriteMap[_sprite] = spriteID ;
                else if (_sprite.name == FilePaths.spriteFilenameWeaponSpawn)
                    spriteMap[_sprite] = spriteID ;
                else if (_sprite.name == FilePaths.spriteFilenameCam)
                    spriteMap[_sprite] = spriteID ;
            }
            //every time when sprite is read,spriteID increments to be unique for next reding
        */    
        spriteID++;
        }        
        return spriteMap;
    }    

   /* public static int GetSpriteID(GameObject obj)
    {
        foreach(KeyValuePair<int,EntityInfo> _key in ResourceReader.platformSpriteMap)  {
            if()
        }
    }
    */


}
