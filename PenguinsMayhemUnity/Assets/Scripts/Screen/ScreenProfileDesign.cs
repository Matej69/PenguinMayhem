using UnityEngine;
using System.Collections;

public class ScreenProfileDesign : Screen {

    public GameObject profileChoiceCanvas;

    public ScreenProfileDesign()
    {
        screenType = ScreenType.CHARACTER_CHOICE;
        profileChoiceCanvas = MonoBehaviour.Instantiate(Resources.Load(FilePaths.canvasProfileChoice), new Vector2(0, 0), Quaternion.identity) as GameObject;
    }


    public override void DestroyNonPassableResources()
    {
        MonoBehaviour.Destroy(profileChoiceCanvas);
    }

    public override void DestroyPassableResources()
    {
    }
    public override void UpdateScreen()
    {
    }
}
