using UnityEngine;
using System.Collections;

public class ScreenEditor : Screen
{

    //::::::::::::::::::::::::::::::::
    //MONOBEHAVIOUR OBJECT::::::::::::
    //::::::::::::::::::::::::::::::::
    [HideInInspector]
    public int cellSize = 32;

    private GameObject toolsMenu;
    public GameObject mapEditor;   

    //::::::::::::::::::::::::::::::::
    //SCREEN_MAIN PROPERTIES::::::::::  
    //::::::::::::::::::::::::::::::::


    public ScreenEditor()
    {        
        screenType = ScreenType.EDITOR;
        toolsMenu = MonoBehaviour.Instantiate(Resources.Load(FilePaths.canvasEditor), new Vector2(0, 0), Quaternion.identity) as GameObject;
        mapEditor = MonoBehaviour.Instantiate(Resources.Load(FilePaths.mapEditor), new Vector2(0, 0), Quaternion.identity) as GameObject;
                
        Profile.s_profiles.Add(new Profile());
    }


    public override void DestroyNonPassableResources()
    {
        MonoBehaviour.Destroy(toolsMenu);
        MonoBehaviour.Destroy(mapEditor);
    }

    public override void DestroyPassableResources()
    {
    }

    public override void UpdateScreen()
    {
    }

}
