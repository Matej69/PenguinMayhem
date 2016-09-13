using UnityEngine;
using System.Collections;

public class EntityInfo {

    public enum entityType { PLATFORM, BACKGROUND, ITEM/* PLAYER_SPAWN, WEAPON_SPAWN, CAM_POS*/ };


    //public static EntityInfo infoPlatform = new EntityInfo(true, entityType.PLATFORM);
    //public static EntityInfo infoBackground = new EntityInfo(false, entityType.BACKGROUND);
    //public static EntityInfo infoItem = new EntityInfo(false, entityType.);


    //public Sprite sprite;
    public bool isCollidable;
    public entityType type;
    public int spriteID;

    public EntityInfo(int _spriteID, bool _isCollidable, entityType _type)
    {
        spriteID = _spriteID;
        isCollidable = _isCollidable;
        type = _type;
    }

    public static EntityInfo GetInfoFromObject(GameObject obj)
    {
        Sprite objSprite = obj.GetComponent<SpriteRenderer>().sprite;
        //info variables
        int _spriteID = 0;
        bool _isCollidable = false;
        entityType _type = entityType.PLATFORM;
        //check in what dictionary key is and get its value
        if (ResourceReader.platformSpriteMap.ContainsKey(objSprite)) {
            _spriteID = ResourceReader.platformSpriteMap[objSprite];
            _isCollidable = true;
            _type = entityType.PLATFORM;
        }else if (ResourceReader.backgroundSpriteMap.ContainsKey(objSprite)) {
            _spriteID = ResourceReader.backgroundSpriteMap[objSprite];
            _isCollidable = false;
            _type = entityType.BACKGROUND;
        }else if (ResourceReader.itemSpriteMap.ContainsKey(objSprite)) {
            _spriteID = ResourceReader.itemSpriteMap[objSprite];
            _isCollidable = false;
            _type = entityType.ITEM;
        }

        return new EntityInfo(_spriteID, _isCollidable, _type);
    }

    public static void PrintObjectInfo(GameObject obj)
    {
        EntityInfo info = GetInfoFromObject(obj);
        Debug.Log("SPRITE : [" + info.spriteID + " COLLIDABLE : " + info.isCollidable + " TYPE : " + info.type);
    }
       
}
