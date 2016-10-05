using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {

    SoundManager soundManagScript;

    Sprite[] sprites;
    int currFrameID = 0;
    Timer spriteFrameTimer;

    void Awake(){
        soundManagScript = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
    }

	// Use this for initialization
	void Start () {
        spriteFrameTimer = new Timer(0.05f);
        SetAnimation();
        soundManagScript.PlaySplash();

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
