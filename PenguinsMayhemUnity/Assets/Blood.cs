using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

    Sprite[] sprites;
    int currFrameID = 0;
    Timer spriteFrameTimer;

	// Use this for initialization
	void Start () {
        spriteFrameTimer = new Timer(0.05f);
        SetAnimation();

    }
	
	// Update is called once per frame
	void Update () {
        spriteFrameTimer.Tick(Time.deltaTime);
        if (AnimHasNext() && spriteFrameTimer.IsFinished()){            
            RunAnimation();
        }
        else if(!AnimHasNext()){
            Destroy(gameObject);
        }
	
	}


    void SetAnimation() {
        sprites = Resources.LoadAll<Sprite>(FilePaths.spriteBlood);
    }

    bool AnimHasNext(){
        return currFrameID + 1 != sprites.Length;                    
    }

    void RunAnimation()
    {        
        GetComponent<SpriteRenderer>().sprite = sprites[currFrameID];
        currFrameID++;
        spriteFrameTimer.Reset();
    }


}
