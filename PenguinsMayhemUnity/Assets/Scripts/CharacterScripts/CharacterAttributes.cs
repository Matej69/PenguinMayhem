using UnityEngine;
using System.Collections;

public class CharacterAttributes : MonoBehaviour {

    //profile component will get asigned in 'Profile.class' after object creation 
    public GameObject bloodPrefab;
    [HideInInspector]
    public Profile profilePointer;
    [HideInInspector]
    public GameObject Hat;

    private int score;
    
    //public GameObject Gun;

	// Use this for initialization
	void Start () {
        Hat = transform.FindChild("Hat").gameObject;
                	
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsCharacterInBounds()) {             
            profilePointer.score--;
            DestroyObject(1);
        }
    }
    

    
    //**************** SET HAT SPRITE *****************
    public void SetHat(Sprite _sprite) {
        Hat.GetComponent<SpriteRenderer>().sprite = _sprite;
    }

    public void DestroyObject(int bloodDir)
    {
        //spawning blood object
        GameObject bloodObj = (GameObject)Instantiate(bloodPrefab, transform.position, Quaternion.identity);
        Vector2 bloodScale = bloodObj.transform.localScale;
        bloodObj.transform.localScale = new Vector2((bloodDir) * Mathf.Abs(bloodScale.x), bloodScale.y);

        //remove object from list and destroy it
        GameObject thisObj = gameObject;
        ((ScreenGame)Screen.screen).RemoveCharacterFromList(ref thisObj);
        Destroy(gameObject);
    }

    bool IsCharacterInBounds() {
        if (GameObject.Find("BackgroundObject(Clone)") == null)
            return false;
        SpriteRenderer bckgrndSpriteRend = GameObject.Find("BackgroundObject(Clone)").GetComponent<SpriteRenderer>();

        Bounds backgroundBound = bckgrndSpriteRend.bounds;
        Vector2 pos = transform.position;

        if (pos.x < backgroundBound.max.x &&
            pos.x > backgroundBound.min.x &&
            pos.y > backgroundBound.min.y &&
            pos.y < backgroundBound.max.y )
            return true;
        return false;
    }

}
