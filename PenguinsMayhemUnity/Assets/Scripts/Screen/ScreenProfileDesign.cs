using UnityEngine;
using System.Collections;

public class ScreenProfileDesign : Screen {

    public GameObject profileChoiceCanvas;

    private SoundManager soundManScript;

    public ScreenProfileDesign()
    {
        screenType = ScreenType.CHARACTER_CHOICE;
        profileChoiceCanvas = MonoBehaviour.Instantiate(Resources.Load(FilePaths.canvasProfileChoice), new Vector2(0, 0), Quaternion.identity) as GameObject;

        soundManScript = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManScript.PlayBackgroundMusic();
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
