using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public GameObject characterOwner;   //set in 'CharControler when prefab Bullet is instantiated'

    public delegate void DELEGATE();
    DELEGATE fp_Movement;

    Vector2 velocity;
    Vector2 startPosition;

    int Xdir = 1;

    float speedX = 12;
    float speedY = 2;
    float magn = 1;
    void SetValues(float sx,float sy, float mag) {
        speedX = sx;    speedY = sy;    magn = mag;
    }

    // Use this for initialization
    void Start () {
        startPosition = transform.position;

        velocity = new Vector2(0, 0f);
        Xdir = (characterOwner.transform.localScale.x > 0) ? 1 : -1;


        ChooseMoveFunc();

    }
	
	// Update is called once per frame
	void Update () {
        fp_Movement();
        Move();
        
    }
        

    void Move(){
        transform.Translate(velocity * Time.deltaTime);
    }
        
    //********* CHOOSE MOVEMENT FUNCTION
    void ChooseMoveFunc(){
        WeaponInfo.weaponType wType = characterOwner.transform.FindChild("Gun").GetComponent<Weapon>().weaponInfo.type;
        float randSpeedY = (float)Random.Range(-10, 20) / 10;
        switch (wType)
        {
            case WeaponInfo.weaponType.REVOLVER:{   SetValues(4, 0, 0);     fp_Movement = HorMovement;      }break;
            case WeaponInfo.weaponType.AK47:{       SetValues(12, 0, 0);    fp_Movement = HorMovement;      }break;
            //case WeaponInfo.weaponType.GRANADE:{    SetValues(4, 8, 1);     fp_Movement = CosMovement;      }break;
            case WeaponInfo.weaponType.SHOTGUN:{    SetValues(10, randSpeedY, 0);     fp_Movement = ShotGunMovement;  }break;
            case WeaponInfo.weaponType.SPACEPISTOL:{SetValues(2, 0, 0);     fp_Movement = HorMovement;      }break;
            case WeaponInfo.weaponType.UZI:{        SetValues(15, 0, 0);    fp_Movement = HorMovement;      }break;
        }
    }

    //********* MOVEMENT_FUNCTIONS **********   
    void ShotGunMovement() {
        velocity.y = speedY;
        velocity.x = speedX * Xdir;
    }

    void HorMovement() { 
        velocity.x = speedX * Xdir;
    }
    
    void CosMovement() {      
        float holdSin;
        float sin = 0;
        float magnitude = 4;        

        holdSin = sin;
        sin = Mathf.Cos(Time.time * speedY);
        velocity.y = sin * magnitude;
        velocity.x = speedX * Xdir;
    }

    void ThrowMovement(float _speedX, float _speedY, float _mag)
    {
        float Yacc = 0.07f;
        velocity.y = speedY -= Yacc;
        velocity.x = speedX * Xdir;
    }

    //************** TRAIL FUNCTION *************



    //************* BULLET COLLISION ***************
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.layer == 21) {
            //increase score of bullet owner
            characterOwner.GetComponent<CharacterAttributes>().profilePointer.score++;
            //Destroy blood, on destruction blood will be Instantiated
            Vector2 playerPos = col.gameObject.transform.position;
            int scaleX = (playerPos.x > transform.position.x) ? 1 : -1;            
            col.gameObject.GetComponent<CharacterAttributes>().DestroyObject(scaleX);
            //destroy bullet
            Destroy(gameObject);
        }
    }


}
