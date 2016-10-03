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

        if (Screen.screen.screenType == Screen.ScreenType.GAME && ((ScreenGame)(Screen.screen)).Characters.Count != 0)
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

    void SetDynamicCamPos()
    {
        SetBoundsFromCharPos();
        float finalX = (charDistCords.maxX + charDistCords.minX ) / 2;
        float finalY = (charDistCords.maxY + charDistCords.minY) / 2;

        Vector3 targetPos = new Vector3(finalX, finalY,-10);
        //gameObject.transform.position = new Vector3(finalX, finalY, -10);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * camFollowSpeed);
    }

    static public void ZoomIn(float endZoomValue){
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endZoomValue, Time.deltaTime);
    }
    static public void ResetZoom(){
        Camera.main.orthographicSize = startZoomValue;
    }

}
