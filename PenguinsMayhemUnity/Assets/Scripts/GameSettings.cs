using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {
    
    public GameObject InputNumOfRounds;
    public GameObject buttonStartGame;

	// Use this for initialization
	void Start () {
        ButtonStartListener();
    }
	
	// Update is called once per frame
	void Update () {
        
            
	
	}

    void ButtonStartListener()
    {
        Button button = buttonStartGame.GetComponent<Button>();
        button.onClick.AddListener(
            delegate {                
                Screen.ChangeTo(Screen.ScreenType.GAME);                
            });
    }

    public int GetNumOfRoundsFromInput()
    {
        string roundsText = InputNumOfRounds.transform.FindChild("Text").gameObject.GetComponent<Text>().text;        
        int numOfRounds;
        if(int.TryParse(roundsText, out numOfRounds)) {
            return int.Parse(roundsText);
        }else{
            return 10;
         }
    }

}
