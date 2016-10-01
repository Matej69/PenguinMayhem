using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    
    Sprite[] sprites;
    int currSpriteID = 0;

    bool isAnimSet = false;
    Timer frameTimer;

    GameObject ownerObj;

	// Use this for initialization
	void Start () {
        frameTimer = new Timer(0.055f);
        SetAnimation();

    }
	
	// Update is called once per frame
	void Update () {

        if (isAnimSet)  {
            //by every tick, update timers time and change animation
            if (frameTimer.IsFinished()){
                RunAnimation();
                frameTimer.Reset();
            }
            frameTimer.Tick(Time.deltaTime);
            //if we are on last frame, destroy object
            if (IsAnimDone()){
                Destroy(gameObject);
            }
        }
                

    }



    //set proper owner on initialization, NOT HERE
    public void SetOwner(ref GameObject characterobj)
    {
        ownerObj = characterobj;
    }

    //set proper animation is called when explosion is instantiated, NOT HERE
    public void SetAnimation()
    {
        sprites = Resources.LoadAll<Sprite>(FilePaths.spriteExplosion);
        if (sprites == null)
            return;

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

    public bool IsAnimDone()
    {
        return currSpriteID == sprites.Length;
    }




    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 21)
        {
            if (ownerObj != null)
            {
                //increase score of bullet owner if hit object was not owner itself
                if (ownerObj == col.gameObject)
                    return;
                ownerObj.GetComponent<CharacterAttributes>().profilePointer.score++;
            }
            //Destroy character obj, on destruction blood will be Instantiated
            Vector2 playerPos = col.gameObject.transform.position;
            int scaleX = (playerPos.x > transform.position.x) ? 1 : -1;
            col.gameObject.GetComponent<CharacterAttributes>().DestroyObject(scaleX);
            //destroy explosion
        }
    }




}
