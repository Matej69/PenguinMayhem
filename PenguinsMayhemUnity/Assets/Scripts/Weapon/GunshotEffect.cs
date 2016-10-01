using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunshotEffect : MonoBehaviour {

    //when creating gun effect, type of weapon is passed there, NOT HERE
    Sprite[] sprites;
    int currSpriteID = 0;

    bool isAnimSet = false;
    Timer frameTimer;    
        

	// Use this for initialization
	void Start () {
               

    }
	
	// Update is called once per frame
	void Update () {

        if (isAnimSet) {
            //ba every tick, update timers time and change animation
            if (frameTimer.IsFinished()) {
                RunAnimation();
                frameTimer.Reset();                
            }
            frameTimer.Tick(Time.deltaTime);
            //if we are on last frame, destroy object
            if (IsAnimDone()) {
                Destroy(gameObject);
            }
        }

        
	
	}

    //set proper animation is called when gunshot is instantiated, NOT HERE
    public void SetProperAnimation(WeaponInfo wInfo)
    {
        frameTimer = new Timer(wInfo.shotEffectSpeed);
        switch (wInfo.type) {
            case WeaponInfo.weaponType.UZI:{
                    sprites = Resources.LoadAll<Sprite>(FilePaths.spriteShotEffect + "NormalShotEffect");
                }break;       
            case WeaponInfo.weaponType.SHOTGUN:{
                    sprites = Resources.LoadAll<Sprite>(FilePaths.spriteShotEffect + "ShotgunSmokeEffect");
                }break;      
            case WeaponInfo.weaponType.SPACEPISTOL:{
                    sprites = Resources.LoadAll<Sprite>(FilePaths.spriteShotEffect + "SpacegunEffect");
                }break;   
            case WeaponInfo.weaponType.REVOLVER:{
                    sprites = Resources.LoadAll<Sprite>(FilePaths.spriteShotEffect + "RevolverSmokeEffect");
                }break; 
            case WeaponInfo.weaponType.AK47:{
                    sprites = Resources.LoadAll<Sprite>(FilePaths.spriteShotEffect + "Ak47ShotEffect");
                }break; 
        }
        if (sprites == null)
            return;

        //sometimes rotate on y axis so fire effect would look diverse
        int yScale = (int)Mathf.Sign(Random.Range(-3, 3));
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * yScale);

        //set to first frame, so  we dont need to wait for effect to appear on frame 1                 
        GetComponent<SpriteRenderer>().sprite = sprites[currSpriteID];        

        isAnimSet = true;
    }

    public void RunAnimation()
    {
        currSpriteID++;
        if (IsAnimDone())
            return;
        GetComponent<SpriteRenderer>().sprite = sprites[currSpriteID];        
    }

    public bool IsAnimDone(){
        return currSpriteID == sprites.Length;
    }

    public void SetScaleXDir(int dir){
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y);
    }
    
}
