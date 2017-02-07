using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

    GameObject ownerCharacter;
    SoundManager soundManagerScript;

    [HideInInspector]
    public GameObject bulletSpawn;
    public GameObject bulletPrefab;
    public GameObject shotEffectPrefab;
    [HideInInspector]
    public List<GameObject> bullets = new List<GameObject>();
    
    public SpriteRenderer weaponSpriteR;

    public WeaponInfo weaponInfo = new WeaponInfo();    //will create WeaponInfo without any values set, .Copy(other) required.....
    [HideInInspector]
    public Timer shotTimer = new Timer(1);  //1 is just inital value, later changed to proper value

    // Use this for initialization
    void Start () {
        ownerCharacter = transform.parent.gameObject;
        weaponSpriteR = GetComponent<SpriteRenderer>();
        soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        bulletSpawn = transform.FindChild("BulletSpawn").gameObject;

        //set initial weapon
        SwitchToWeapon(WeaponInfo.weaponType.REVOLVER);       

    }
	
	// Update is called once per frame
	void Update () {
        shotTimer.Tick(Time.deltaTime);

        //changing weapon for testing purposes?*?*?*?*?*?*?*?
        /*
        if (InputManager.keyPressed[KeyCode.Alpha1]) {
            WeaponInfo.weaponType type = ++weaponInfo.type;
            if (type >= WeaponInfo.weaponType.SIZE) type = 0;
            SwitchToWeapon(type);            
        }
        */
    }


    public void SwitchToWeapon(WeaponInfo.weaponType _weaponType)
    {
        foreach (KeyValuePair<WeaponInfo.weaponType, WeaponInfo> pair in WeaponInfo.allWeaponInfos) {            
            if (pair.Key == _weaponType) {
                weaponInfo.Copy(pair.Value);
                SetWeaponAttFromWeaponInfo();
            }
        }
    }

    void SetWeaponAttFromWeaponInfo()
    {
        weaponSpriteR.sprite = weaponInfo.weaponSprite;
        shotTimer.SetStartTime(weaponInfo.coolDown);
        shotTimer.SetCurrentTime(0);    //0 so as soon as we switch we can shot
    }   

    //SHOOTING AND SHOOTING EFFECTS
    public void InstantiateBullet() {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<SpriteRenderer>().sprite = weaponInfo.bulletSprite;
        bullet.GetComponent<Bullet>().characterOwner = transform.parent.gameObject;
        bullets.Add(bullet);

    }
    public bool HaveAmmunition() {
        return weaponInfo.numOfBullets > 0;
    }
    public bool CanShoot() {
        return HaveAmmunition() && shotTimer.IsFinished();
    }
    public void Shot() {    
        //instantiate X num of bullets    
        if (weaponInfo.type == WeaponInfo.weaponType.SHOTGUN) { 
            InstantiateBullet(); InstantiateBullet(); InstantiateBullet();
        }
        else { 
            InstantiateBullet();
        }
        //create and init shot effect
        GameObject shotEff = (GameObject)Instantiate(shotEffectPrefab, bulletSpawn.transform.position, Quaternion.identity);
        shotEff.GetComponent<GunshotEffect>().SetProperAnimation(weaponInfo);
        shotEff.GetComponent<GunshotEffect>().SetScaleXDir((int)Mathf.Sign(ownerCharacter.transform.localScale.x));
        //play sound (granade sound is on destruction of bullet, not its instantiation)
        if (weaponInfo.type != WeaponInfo.weaponType.GRENADE)
            soundManagerScript.PlaySound(weaponInfo.type);
        //if it is grenade, hide weapon sprite since it is throwable item
        if (weaponInfo.type == WeaponInfo.weaponType.GRENADE)
            weaponSpriteR.sprite = null;

        ApplyThrowback();
        weaponInfo.numOfBullets--;
        shotTimer.Reset();        
    }

    void ApplyThrowback() {
        CharacterController controllerScript = ownerCharacter.GetComponent<CharacterController>();
        if (controllerScript.IsFacingRight())
            controllerScript.velocity.x = -weaponInfo.throwBackAmount;
        else
            controllerScript.velocity.x = weaponInfo.throwBackAmount;
    }




}
