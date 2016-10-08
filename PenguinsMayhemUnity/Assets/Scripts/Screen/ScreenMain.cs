using UnityEngine;
using System.Collections;

public class ScreenMain : Screen {

    //::::::::::::::::::::::::::::::::
    //MONOBEHAVIOUR OBJECT::::::::::::
    //::::::::::::::::::::::::::::::::

    private GameObject mainMenu;

    //::::::::::::::::::::::::::::::::
    //SCREEN_MAIN PROPERTIES::::::::::  
    //::::::::::::::::::::::::::::::::

    public ScreenMain() {
        screenType = ScreenType.MAIN;
        mainMenu = MonoBehaviour.Instantiate(Resources.Load(FilePaths.canvasMenu), new Vector2(0, 0), Quaternion.identity) as GameObject;
    }
    

    public override void DestroyNonPassableResources() {
        mainMenu.GetComponent<GUIMenu>().DestroyGUI();
        MonoBehaviour.Destroy(mainMenu);
    }

    public override void DestroyPassableResources() {       
    }
    public override void UpdateScreen()
    {
    }

}
