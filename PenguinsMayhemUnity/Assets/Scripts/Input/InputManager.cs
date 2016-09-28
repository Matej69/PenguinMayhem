using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [HideInInspector]
    public static Dictionary<KeyCode, bool> keyPressed = new Dictionary<KeyCode, bool>();
    public static Dictionary<KeyCode, bool> keyHold = new Dictionary<KeyCode, bool>();
    public static Dictionary<KeyCode, bool> keyReleased = new Dictionary<KeyCode, bool>();

    [HideInInspector]
    public static Vector2 mousePos;

    // Use this for initialization
    void Awake () {
        SetAllKeysToFalse();        
    }
    
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetKeyInput();  

    }

    void SetAllKeysToFalse()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
            keyPressed[key] = false;
            keyHold[key] = false;
            keyReleased[key] = false;
        }
    }

    void GetKeyInput()
    {
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            //reset state
            keyReleased[key] = false;
            //change key check state depends on events unity recieves from keyboard
            if (Input.GetKeyDown(key) && !keyPressed[key])
            {
                keyPressed[key] = true;
            }
            else if (keyPressed[key])
            {
                keyHold[key] = true;
                keyPressed[key] = false;
            }
            if (Input.GetKeyUp(key) && keyHold[key] && !Input.GetKey(key))
            {
                keyReleased[key] = true;
                keyPressed[key] = false;
                keyHold[key] = false;
            }
        }
    }

}
