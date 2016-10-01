using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class WeaponInfo {

    public enum weaponType { REVOLVER, AK47, SHOTGUN, SPACEPISTOL, UZI, GRENADE, SIZE };

    //STATIC STUFF
    public static Dictionary<weaponType, WeaponInfo> allWeaponInfos = new Dictionary<weaponType, WeaponInfo>();   
    static WeaponInfo() {
        allWeaponInfos[weaponType.REVOLVER] = new WeaponInfo(weaponType.REVOLVER,GetWeaponSprite("revolver"),GetBulletSprite("revolver_bullet"),null,6,0.3f,0.05f,0.01f);
        allWeaponInfos[weaponType.AK47] = new WeaponInfo(weaponType.AK47,GetWeaponSprite("ak47"),GetBulletSprite("ak47_bullet"), null, 15, 0.1f, 0.05f, 0.1f);
        allWeaponInfos[weaponType.SHOTGUN] = new WeaponInfo(weaponType.SHOTGUN, GetWeaponSprite("shotgun"), GetBulletSprite("shotgun_bullet"), null, 5, 0.5f, 0.04f, 0.2f);
        allWeaponInfos[weaponType.SPACEPISTOL] = new WeaponInfo(weaponType.SPACEPISTOL, GetWeaponSprite("spacepistol"), GetBulletSprite("spacepistol_bullet"), null, 5, 0.5f, 0.04f, 0.01f);
        allWeaponInfos[weaponType.UZI] = new WeaponInfo(weaponType.UZI, GetWeaponSprite("uzi"), GetBulletSprite("uzi_bullet"), null, 20, 0.05f, 0.04f, 0.08f);
        allWeaponInfos[weaponType.GRENADE] = new WeaponInfo(weaponType.GRENADE, GetWeaponSprite("grenade"), GetWeaponSprite("grenade"), null, 1, 0.1f, 0.04f, 0.01f);
    }

    public static Sprite GetWeaponSprite(string weaponName) {
        return Resources.Load<Sprite>(FilePaths.spriteWeapon + weaponName);
    }
     public static Sprite GetBulletSprite(string bulletName) {
        return Resources.Load<Sprite>(FilePaths.spriteBullet + bulletName);
    }

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
