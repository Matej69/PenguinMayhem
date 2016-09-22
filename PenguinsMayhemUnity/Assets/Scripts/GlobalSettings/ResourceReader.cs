using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ResourceReader {

    private static int spriteID = 0;

    //public static Sprite[] platformSprites = GetSprites(FilePaths.spritePlatform);
    //public static Sprite[] entitySprites = GetSprites(FilePaths.spriteEntity);

    //public static EntityInfo platformInfo = new EntityInfo()

    public static List<Dictionary<Sprite, int>> dictionarySpriteIDList = new List<Dictionary<Sprite, int>>();
    public static Dictionary<Sprite, int> platformSpriteMap = GetSpritesFromFolder(FilePaths.spritePlatform);
    public static Dictionary<Sprite, int> backgroundSpriteMap = GetSpritesFromFolder(FilePaths.spriteBackground);
    public static Dictionary<Sprite, int> itemSpriteMap = GetSpritesFromFolder(FilePaths.spriteItem);    

    /*
    public static GameObject[] GetGameObjects(string _path)
    {
        GameObject[] objects = Resources.LoadAll<GameObject>(_path);
        Debug.Log(objects.Length);
        return objects;
    }
*/
    public static Dictionary<Sprite, int> GetSpritesFromFolder(string _path)
    {
        Dictionary<Sprite, int> spriteMap = new Dictionary<Sprite, int>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(_path);

         
        foreach (Sprite _sprite in sprites)
        {
            spriteMap[_sprite] = spriteID;
            spriteID++;
        }
        dictionarySpriteIDList.Add(spriteMap);
        return spriteMap;
    }

    //*******************************************************
    //get Sprite/ID from map of sprites and IDs
    //*******************************************************
    public static int GetSpriteIDFromGameObject(GameObject obj)
    {        
        
        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;
        foreach(Dictionary<Sprite, int> dictionary in dictionarySpriteIDList)  {
            foreach (KeyValuePair<Sprite,int> pair in dictionary) {
                if (sprite != null && sprite.name == pair.Key.name)
                {
                    Debug.Log(dictionary[sprite]);
                    return dictionary[sprite];
                }
            }
        }
        return 707;
    }

    public static Sprite GetSpriteFromSpriteID(int spriteID)
    {        
        foreach (Dictionary<Sprite, int> dictionary in dictionarySpriteIDList)
        {            
            foreach (KeyValuePair<Sprite, int> pair in dictionary)
            {
                if (pair.Value == spriteID)
                    return pair.Key;
            }
        }
        return null;
    }

    //*******************************************************
    //get all hat Sprites as SpriteList
    //*******************************************************
    public static List<Sprite> GetHatSprites()
    {
        Sprite[] hatsArray = Resources.LoadAll<Sprite>(FilePaths.spriteHat);
        List<Sprite> hatsList = new List<Sprite>();
        foreach (Sprite hat in hatsArray) {
            hatsList.Add(hat);
        }
        return hatsList;
    }



}
