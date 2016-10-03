using UnityEngine;
using System.Collections;

public class Confetti : MonoBehaviour {

    Vector3 rotateAmount;
    Vector3 fallingAmount;

    Timer destroyTimer;

	// Use this for initialization
	void Start () {
        rotateAmount = new Vector3((float)Random.RandomRange(-50, 50) / 10, (float)Random.RandomRange(-50, 50) / 10, 0);
        fallingAmount = new Vector3((float)Random.RandomRange(-10, 10) / 1000, -(float)Random.RandomRange(10, 100) / 2000, 0);
        destroyTimer = new Timer(15);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", GetRandColor());
    }
	
	// Update is called once per frame
	void Update () {
        ScaleUpdate();
        MoveUpdate();

        destroyTimer.Tick(Time.deltaTime);
        DestroyConfetti();

    }


    Color GetRandColor() {
        Color retColor = new Color(0,0,0);
        //which RGB component will stay 1
        int RGBMax = Random.Range(0, 3);
        float randRGB1 = (float)Random.Range(0, 100) / 100;
        float randRGB2 = (float)Random.Range(0, 100) / 100;
        switch (RGBMax) {
            case 0: retColor = new Color(1, randRGB1, randRGB2); break;
            case 1: retColor = new Color(randRGB1, 1, randRGB2); break;
            case 2: retColor = new Color(randRGB1, randRGB2, 1); break;
        }
        return retColor; 
    }


    void ScaleUpdate() {
        transform.Rotate(Vector3.up, rotateAmount.y,Space.World);
        transform.Rotate(Vector3.right, rotateAmount.x, Space.World);
        transform.Rotate(Vector3.forward, rotateAmount.z, Space.World);
    }
    void MoveUpdate() {
        transform.Translate(fallingAmount, Space.World);
    }


    void DestroyConfetti() {

        if (destroyTimer.IsFinished() || transform.position.y < CameraScript.camPos.y - CameraScript.camWorldSize.y/2)
            Destroy(gameObject);
    }


}
