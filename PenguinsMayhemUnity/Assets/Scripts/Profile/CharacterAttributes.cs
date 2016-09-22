using UnityEngine;
using System.Collections;

public class CharacterAttributes : MonoBehaviour {

    //profile component will get asigned in 'Profile.class' after object creation 
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


}
