using UnityEngine;
using System.Collections;

public class ResourcesLoadManager : MonoBehaviour {

	void Awake(){

        //SET UP ALL WEAPONS
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.REVOLVER] = new WeaponInfo(WeaponInfo.weaponType.REVOLVER, GetWeaponSprite("revolver"), GetBulletSprite("revolver_bullet"), null, 6, 0.3f, 0.05f, 0.01f);
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.AK47] = new WeaponInfo(WeaponInfo.weaponType.AK47, GetWeaponSprite("ak47"), GetBulletSprite("ak47_bullet"), null, 15, 0.1f, 0.05f, 0.1f);
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.SHOTGUN] = new WeaponInfo(WeaponInfo.weaponType.SHOTGUN, GetWeaponSprite("shotgun"), GetBulletSprite("shotgun_bullet"), null, 5, 0.5f, 0.04f, 0.2f);
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.SPACEPISTOL] = new WeaponInfo(WeaponInfo.weaponType.SPACEPISTOL, GetWeaponSprite("spacepistol"), GetBulletSprite("spacepistol_bullet"), null, 5, 0.5f, 0.04f, 0.01f);
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.UZI] = new WeaponInfo(WeaponInfo.weaponType.UZI, GetWeaponSprite("uzi"), GetBulletSprite("uzi_bullet"), null, 20, 0.05f, 0.04f, 0.08f);
        WeaponInfo.allWeaponInfos[WeaponInfo.weaponType.GRENADE] = new WeaponInfo(WeaponInfo.weaponType.GRENADE, GetWeaponSprite("grenade"), GetWeaponSprite("grenade"), null, 1, 0.1f, 0.04f, 0.01f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public static Sprite GetWeaponSprite(string weaponName) {
        return Resources.Load<Sprite>(FilePaths.spriteWeapon + weaponName);
    }
     public static Sprite GetBulletSprite(string bulletName) {
        return Resources.Load<Sprite>(FilePaths.spriteBullet + bulletName);
    }





}
