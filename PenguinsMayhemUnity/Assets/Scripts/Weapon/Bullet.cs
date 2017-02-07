using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

    SoundManager soundManagerScript;

    public static List<GameObject> bulletsOnScreen = new List<GameObject>();       //refrence when we need to destroy all bullets

    Timer maxTimeAliveTimer;

    public GameObject explosionPrefab;

    [HideInInspector]
    public GameObject characterOwner;   //set in 'CharControler when prefab Bullet is instantiated'
    WeaponInfo.weaponType weaponType;

    public delegate void DELEGATE();
    DELEGATE fp_Movement;

    Vector2 velocity;
    Vector2 startPosition;

    int Xdir = 1;

    float speedX = 12;
    float speedY = 2;
    float magn = 1;
    void SetValues(float sx, float sy, float mag)
    {
        speedX = sx; speedY = sy; magn = mag;
    }

    // Use this for initialization
    void Start()
    {
        weaponType = characterOwner.transform.FindChild("Gun").GetComponent<Weapon>().weaponInfo.type;
        soundManagerScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        bulletsOnScreen.Add(gameObject);

        startPosition = transform.position;

        velocity = new Vector2(0, 0f);
        Xdir = (characterOwner.transform.localScale.x > 0) ? 1 : -1;

        maxTimeAliveTimer = new Timer(7);

        ChooseMoveFunc();

    }

    // Update is called once per frame
    void Update()
    {
        maxTimeAliveTimer.Tick(Time.deltaTime);
        if (maxTimeAliveTimer.IsFinished())
            Destroy(gameObject);

        fp_Movement();
        Move();
        if(weaponType == WeaponInfo.weaponType.GRENADE)
            Rotate(2f * Mathf.Sign(Xdir));

    }


    void Move()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
    void Rotate(float rotSpeed)
    {
        transform.Rotate(Vector3.back * rotSpeed, Space.World);
    }


    //********* CHOOSE MOVEMENT FUNCTION
    void ChooseMoveFunc()
    {
        WeaponInfo.weaponType wType = characterOwner.transform.FindChild("Gun").GetComponent<Weapon>().weaponInfo.type;
        float randSpeedY = (float)Random.Range(-10, 20) / 10;
        switch (wType)
        {
            case WeaponInfo.weaponType.REVOLVER: { SetValues(8, 0, 0); fp_Movement = HorMovement; } break;
            case WeaponInfo.weaponType.AK47: { SetValues(12, 0, 0); fp_Movement = HorMovement; } break;
            //case WeaponInfo.weaponType.GRANADE:{    SetValues(4, 8, 1);     fp_Movement = CosMovement;      }break;
            case WeaponInfo.weaponType.SHOTGUN: { SetValues(10, randSpeedY, 0); fp_Movement = ShotGunMovement; } break;
            case WeaponInfo.weaponType.SPACEPISTOL: { SetValues(2, 0, 0); fp_Movement = HorMovement; } break;
            case WeaponInfo.weaponType.UZI: { SetValues(15, 0, 0); fp_Movement = HorMovement; } break;
            case WeaponInfo.weaponType.GRENADE: { SetValues(7, 7, 0); fp_Movement = ThrowMovement; } break;
        }
    }

    //********* MOVEMENT_FUNCTIONS **********   
    void ShotGunMovement()
    {
        velocity.y = speedY;
        velocity.x = speedX * Xdir;
    }

    void HorMovement()
    {
        velocity.x = speedX * Xdir;
    }

    void CosMovement()
    {
        float holdSin;
        float sin = 0;
        float magnitude = 4;

        holdSin = sin;
        sin = Mathf.Cos(Time.time * speedY);
        velocity.y = sin * magnitude;
        velocity.x = speedX * Xdir;
    }

    void ThrowMovement()
    {
        float Yacc = 0.4f;
        velocity.y = speedY -= Yacc;
        velocity.x = speedX * Xdir;
    }
        
    //************** TRAIL FUNCTION *************



    //************* BULLET COLLISION ***************
    void OnTriggerEnter2D(Collider2D col)
    {
        if (characterOwner == null)
            return;

        //for bullets
        if (weaponType != WeaponInfo.weaponType.GRENADE)
        {
            if (col.gameObject.layer == 20){
                Destroy(gameObject);
            }
            if (col.gameObject.layer == 21) {
                //if we collided with owner, don't do anything
                if (characterOwner == col.gameObject)
                    return;
                characterOwner.GetComponent<CharacterAttributes>().profilePointer.score++;
                //Destroy blood, on destruction blood will be Instantiated
                Vector2 playerPos = col.gameObject.transform.position;
                int scaleX = (playerPos.x > transform.position.x) ? 1 : -1;
                col.gameObject.GetComponent<CharacterAttributes>().DestroyObject(scaleX);
                //destroy bullet
                Destroy(gameObject);
               }
        }

        //for granade on platform/palyer collision
        else if (weaponType == WeaponInfo.weaponType.GRENADE)
        {
            if (col.gameObject.layer == 20 || col.gameObject.layer == 21)
            {                
                //Destroy character obj, on destruction blood will be Instantiated
                GameObject explosionObj = (GameObject)Instantiate(explosionPrefab,gameObject.transform.position, Quaternion.identity);
                explosionObj.GetComponent<Explosion>().SetOwner(ref characterOwner);
                //play sound
                soundManagerScript.PlaySound(WeaponInfo.weaponType.GRENADE);
                //destroy bullet
                Destroy(gameObject);
            }
        }

    }



}
