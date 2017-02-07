using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponInfo {

    public enum weaponType { REVOLVER, AK47, SHOTGUN, SPACEPISTOL, UZI, GRENADE, SIZE };

    //******** STATIC STUFF *********
    //Dictionary of all weaponInfos is set-up in 'resourceLoadManager' that is in scene because we can not
    //load Resources on static constructor.... trust me i tried everything and nothing works and im not gonna go to deep
    //web to the darkest place of internet with 666 numbers insted of classic browser search bar just to find solution
    //thanks for reading now just go away, you should probably do something useful, and if you are me then be better at ur job next time
    public static Dictionary<weaponType, WeaponInfo> allWeaponInfos = new Dictionary<weaponType, WeaponInfo>();
     
    

    //REGULAR STUFF
    public weaponType type;
    public Sprite weaponSprite, bulletSprite, shotEffectSprite;
    public int numOfBullets;
    public float coolDown;
    public float shotEffectSpeed;
    public float throwBackAmount;

   
    public WeaponInfo() {
    }
    
    public WeaponInfo(weaponType _type,Sprite gunS, Sprite bulletS, Sprite shotEffectS, int bulletNum, float _coolDown, float shotEfSpeed,float trwBack){
       type = _type;
       weaponSprite = gunS;
       bulletSprite = bulletS;
       shotEffectSprite = shotEffectS;
       numOfBullets = bulletNum;
       coolDown = _coolDown;
       shotEffectSpeed = shotEfSpeed;
       throwBackAmount = trwBack;
   }
   
    
    public void Copy(WeaponInfo other) {
        type = other.type;
        weaponSprite = other.weaponSprite;
        bulletSprite = other.bulletSprite;
        shotEffectSprite = other.shotEffectSprite;
        numOfBullets = other.numOfBullets;
        coolDown = other.coolDown;
        shotEffectSpeed = other.shotEffectSpeed;
        throwBackAmount = other.throwBackAmount;
    }
        



}
