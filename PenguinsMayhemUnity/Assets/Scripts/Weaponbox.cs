using UnityEngine;
using System.Collections;

public class Weaponbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 21) {
            Weapon weaponScript = col.gameObject.transform.FindChild("Gun").gameObject.GetComponent<Weapon>();
            int weaponID = Random.Range(0, (int)WeaponInfo.weaponType.SIZE);
            weaponScript.SwitchToWeapon((WeaponInfo.weaponType)weaponID);
            Destroy(gameObject);
        }
    }



}
