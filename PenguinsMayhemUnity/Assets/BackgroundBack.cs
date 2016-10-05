using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundBack : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public GameObject movableObjPrefab;

    enum backgroundType { SKY, WATER,SIZE }
    backgroundType backgrType;

    public List<Sprite> mountainsSprites;
    public Sprite waterSprite;
    public List<Sprite> fishSprites;
    public Sprite cloudSprite;

    private List<GameObject> movableObjs;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        movableObjs = new List<GameObject>();
    }

	// Use this for initialization
	void Start () {
        backgrType = (backgroundType)Random.Range(0, (int)backgroundType.SIZE);
        SetBackground();
        AddMovables();

    }
	
	// Update is called once per frame
	void Update () {
        
    }


    void SetBackground() {
        switch (backgrType) {
            case backgroundType.SKY:
                {
                    int rndNum = Random.RandomRange(0, mountainsSprites.Count);
                    spriteRenderer.sprite = mountainsSprites[rndNum];                    
                }
                break;
            case backgroundType.WATER:
                {
                    spriteRenderer.sprite = waterSprite;
                }
                break;
        }
    }

    Sprite LoadBackgroundSprite(string _name){
        return Resources.Load<Sprite>(FilePaths.spriteBackgroundBack+_name);
    }

    void AddMovables(){
        int numOfObj = Random.Range(2, 6);
        for(int i = 0; i < numOfObj; ++i) {
            int randX = Random.Range((int)CameraScript.camPos.x - 10, (int)CameraScript.camPos.x / 2 + 10);
            int randY = Random.Range((int)CameraScript.camPos.y - 7, (int)CameraScript.camPos.y + 7);
            movableObjs.Add((GameObject)Instantiate(movableObjPrefab,new Vector3(randX,randY,-6),Quaternion.identity));
            //add sprite
            if(backgrType == backgroundType.SKY) {
                movableObjs[movableObjs.Count - 1].GetComponent<SpriteRenderer>().sprite = cloudSprite;
            }
            if(backgrType == backgroundType.WATER) {
                int randSpriteID = Random.Range(0, fishSprites.Count);
                movableObjs[movableObjs.Count - 1].GetComponent<SpriteRenderer>().sprite = fishSprites[randSpriteID];
            }
        }
        
    }

    public void DestroyAllBackgroundObject() {
        foreach(GameObject obj in movableObjs) {
            Destroy(obj);
        }
        movableObjs.Clear();
        Destroy(gameObject);
    }


}
