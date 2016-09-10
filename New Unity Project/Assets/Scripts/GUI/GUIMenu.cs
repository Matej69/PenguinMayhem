using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour {

    private GameObject startButton;
    private GameObject optionsButton;
    private GameObject exitButton;

    // Use this for initialization
    void Start () {
        startButton     =   gameObject.transform.FindChild("Start").gameObject;
        optionsButton   =   gameObject.transform.FindChild("Options").gameObject;
        exitButton      =   gameObject.transform.FindChild("Exit").gameObject;

        //OnButtonClickListeners setup
        startButton.GetComponent<Button>().onClick.AddListener(OnStart);
        optionsButton.GetComponent<Button>().onClick.AddListener(OnOptions);
        exitButton.GetComponent<Button>().onClick.AddListener(OnExit);

    }

    void OnStart() {
        //go to next screen
    }

    void OnOptions() {
        
    }

    void OnExit() {
        Application.Quit();
        Debug.Log("ss");
    }

}
