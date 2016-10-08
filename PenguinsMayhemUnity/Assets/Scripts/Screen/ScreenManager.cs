using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScreenManager : MonoBehaviour {
    


    void Start () {
        Screen.screen = new ScreenEditor();
        //Screen.screen = new ScreenProfileDesign();
    }

    void Update () {
        Screen.screen.UpdateScreen();
    }
    

}
