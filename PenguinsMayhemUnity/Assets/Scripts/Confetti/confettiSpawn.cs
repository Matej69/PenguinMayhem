using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class confettiSpawn : MonoBehaviour {

    public GameObject cubePrefab;
    List<GameObject>  confetti;

    Timer createConfettiTimer;

	// Use this for initialization
	void Start () {
        confetti = new List<GameObject>();
        createConfettiTimer = new Timer(0.1f);
        

    }


	
	// Update is called once per frame
	void Update () {
        createConfettiTimer.Tick(Time.deltaTime);
        if (createConfettiTimer.IsFinished()) {
            confetti.Add((GameObject)Instantiate(cubePrefab, GetRandPosition(),Quaternion.identity));
            createConfettiTimer.Reset();
        }
    }
    

    Vector3 GetRandPosition() {
        float min = CameraScript.camPos.x - CameraScript.camWorldSize.x / 2;
        float max = CameraScript.camPos.x + CameraScript.camWorldSize.x / 2;
        float randX = Random.Range(min, max);
        return new Vector3(randX, CameraScript.camPos.y + CameraScript.camWorldSize.y/2, 10);
    }   

    //*** DESTROY FUNCTIONS ***
    void DestroyAllConfetti() {
        for(int i = confetti.Count-1; i >= 0; --i) {
            if (confetti[i] != null)
                Destroy(confetti[i]);
        }
        confetti.Clear();
    }

    public void DestroySpawner() {
        DestroyAllConfetti();
        Destroy(gameObject);
    }

}
