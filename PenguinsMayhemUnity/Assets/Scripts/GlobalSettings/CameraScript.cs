using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour {

    float camFollowSpeed = 2;

    static float startZoomValue;
    public static Vector3 camPos;
    public static Vector2 camWorldSize;

    struct CharDistanceCord{
        public float minX, minY, maxX, maxY;
        public void SetInitialVal(GameObject obj){
            minX = obj.transform.position.x;
            maxX = obj.transform.position.x;
            minY = obj.transform.position.y;
            maxY = obj.transform.position.y;
        }
    }
    CharDistanceCord charDistCords;


    void Awake() {
        camPos = transform.position;
        startZoomValue = Camera.main.orthographicSize;        
        camWorldSize = new Vector2((2f * Camera.main.orthographicSize) * Camera.main.aspect, 2f * Camera.main.orthographicSize);
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        camPos = transform.position;
        camWorldSize = new Vector2((2f * Camera.main.orthographicSize) * Camera.main.aspect, 2f * Camera.main.orthographicSize);

        if (Screen.screen.screenType == Screen.ScreenType.GAME && ((ScreenGame)(Screen.screen)).Characters.Count > 0)
        {
            SetDynamicCamPos();
        }
        else if (Screen.screen.screenType != Screen.ScreenType.EDITOR)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0,0,-10), Time.deltaTime);
        }

    }

    void SetBoundsFromCharPos() {
        List<GameObject> objList = ((ScreenGame)(Screen.screen)).Characters;
        charDistCords.SetInitialVal(objList[0]);        
        foreach(GameObject obj in objList){
            if (obj.transform.position.y < charDistCords.minY)
                charDistCords.minY = obj.transform.position.y;
            if (obj.transform.position.y > charDistCords.maxY)
                charDistCords.maxY = obj.transform.position.y;
            if (obj.transform.position.x < charDistCords.minX)
                charDistCords.minX = obj.transform.position.x;
            if (obj.transform.position.x > charDistCords.maxX)
                charDistCords.maxX = obj.transform.position.x;
        }
    }
    //**************** READ THIS DESC [=] IMPORTANT STUFF *****************
    //dynamic camera positioning will be applied only if timer is ready
    //timer will be reseted every time our camera hit one of collision bounds
    //reason for that is that if camera is our of bounds and we stoped it and position in bounds again, we do not want to check for dynaimc cam positioning right away
    //becouse camera will be jerking between being positioned in bound when out and being positioned acording to characterGO
    //**************** THANKS FOR READING DUDE, NOW GO AWAY ************************* 
    void SetDynamicCamPos()
    {
        SetBoundsFromCharPos();

        float finalX = (charDistCords.maxX + charDistCords.minX) / 2;
        float finalY = (charDistCords.maxY + charDistCords.minY) / 2;

        Vector3 targetPos = new Vector3(finalX, finalY, -10);
        Vector3 finalPos = transform.position;
        //camera will follow only if it is inside max bounds(bounds of background gameObject)
        if (WillTargetCamPosMakeCamInBounds(targetPos)){            
            finalPos = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * camFollowSpeed);
            transform.position = finalPos;
        }
        
    }
    
    bool WillTargetCamPosMakeCamInBounds(Vector3 _targetPos) {
        if (GameObject.Find("BackgroundObject(Clone)") == null)
            return false;
        SpriteRenderer bckgrndSpriteRend = GameObject.Find("BackgroundObject(Clone)").GetComponent<SpriteRenderer>();
        
        Bounds backgroundBound = bckgrndSpriteRend.bounds;
        
        if (_targetPos.x - camWorldSize.x / 2 > backgroundBound.min.x &&
            _targetPos.x + camWorldSize.x / 2 < backgroundBound.max.x &&
            _targetPos.y - camWorldSize.y / 2 > backgroundBound.min.y &&
            _targetPos.y + camWorldSize.y / 2 < backgroundBound.max.y) {            
            return true;
            }
        return false;
    }       

    //zooming functions
    static public void ZoomIn(float endZoomValue){
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endZoomValue, Time.deltaTime);
    }
    static public void ResetZoom(){
        Camera.main.orthographicSize = startZoomValue;
    }



}
