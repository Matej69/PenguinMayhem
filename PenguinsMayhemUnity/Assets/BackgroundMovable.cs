using UnityEngine;
using System.Collections;

public class BackgroundMovable : MonoBehaviour {
        
    private float speedX;

    void Awake() {
        

    }

	// Use this for initialization
	void Start () {
        //speed
        speedX = (float)Random.Range(5, 60) / 1000;
        //scale(size)
        float randScale = (float)Random.Range(900, 1800) / 1000;
        transform.localScale = new Vector3(randScale, randScale, transform.localScale.z);
        //scale(dir)
        int dir = Random.Range(0, 2);
        if(dir == 0) {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speedX *= -1;
        }
        else{
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }

	}
	
	// Update is called once per frame
	void Update () {
        Move();
        UpdatePosition();
    }

    void Move(){
        transform.position = new Vector2(transform.position.x + speedX, transform.position.y);
    }
    void UpdatePosition() {
        float spawnSkin = 5;
        if(transform.position.x < CameraScript.camPos.x - CameraScript.camWorldSize.x/2 - spawnSkin)
            transform.position = new Vector2(CameraScript.camPos.x + CameraScript.camWorldSize.x / 2 + spawnSkin, transform.position.y);
        else if(transform.position.x > CameraScript.camPos.x + CameraScript.camWorldSize.x / 2 + spawnSkin)
            transform.position = new Vector2(CameraScript.camPos.x - CameraScript.camWorldSize.x / 2 - spawnSkin, transform.position.y);
    }

}
