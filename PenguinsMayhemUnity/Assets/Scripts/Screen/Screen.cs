using UnityEngine;
using System.Collections;

//needs to be MonoBehaviour so we can attach GameObject to Screen instances
public abstract class Screen {

    public static Screen screen;
    public enum ScreenType { MAIN, EDITOR, CHARACTER_CHOICE, GAME };
    public ScreenType screenType;

    public Screen()
    {
    }

    public static void ChangeTo(ScreenType _type)
    {        
        switch (_type)
        {           
            case ScreenType.MAIN:
                {
                    Screen.screen.DestroyNonPassableResources();
                    Screen.screen.DestroyPassableResources();
                    Screen.screen = new ScreenMain();
                }
                break;
            case ScreenType.EDITOR:
                {
                    Screen.screen.DestroyNonPassableResources();
                    Screen.screen.DestroyPassableResources();
                    Screen.screen = new ScreenEditor();
                }
                break;
            case ScreenType.CHARACTER_CHOICE:
                {
                    Screen.screen = new ScreenProfileDesign();
                }
                break;
            case ScreenType.GAME:
                {
                    Screen screenToDestroyHolder = Screen.screen;
                    //if switching to game screen from profileDesign screen
                    if (Screen.screen.screenType == Screen.ScreenType.CHARACTER_CHOICE){
                        Profile.RemoveUnreadyProfiles();
                        int numOfRounds = ((ScreenProfileDesign)(Screen.screen)).profileChoiceCanvas.transform.FindChild("GameSettings").GetComponent<GameSettings>().GetNumOfRoundsFromInput();                        
                        Screen.screen = new ScreenGame(numOfRounds);
                    }
                    screenToDestroyHolder.DestroyNonPassableResources();
                    screenToDestroyHolder.DestroyPassableResources();
                }
                break;

        }
    }

    public abstract void UpdateScreen();

    public abstract void DestroyNonPassableResources ();
    public abstract void DestroyPassableResources ();
}

