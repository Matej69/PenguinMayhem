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

}
