using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour {

    private GameObject start;
    private GameObject options;
    private GameObject exit;
    private GameObject closeMenu;
    private GameObject editor;

    // Use this for initialization
    void Start () {
        start    =   gameObject.transform.FindChild("Start").gameObject;
        options   =   gameObject.transform.FindChild("Options").gameObject;
        exit      =   gameObject.transform.FindChild("Exit").gameObject;
        closeMenu = transform.FindChild("CloseMenu").gameObject;
        editor = transform.FindChild("Editor").gameObject;

        Button startButton = start.GetComponent<Button>();
        startListener(ref startButton);

        Button optionsButton = options.GetComponent<Button>();
        optionsClickListener(ref optionsButton);

        Button exitButton = exit.GetComponent<Button>();
        exitListener(ref exitButton);

        Button closeMenuButton = closeMenu.GetComponent<Button>();
        closeMenuListener(ref closeMenuButton);

        Button editorButton = editor.GetComponent<Button>();
        editorListener(ref editorButton);


    }

    void startListener(ref Button _startBtn) {
        //go to next screen
    }
    void optionsClickListener(ref Button _closeMenu) {
        _closeMenu.onClick.AddListener(
           delegate
           {
               Instantiate(Resources.Load(FilePaths.canvasOptions), new Vector2(0, 0), Quaternion.identity);
           });
    }
    void exitListener(ref Button _exitMenu) {
        _exitMenu.onClick.AddListener(
          delegate
          {
              Application.Quit();
          });
    }
    void closeMenuListener(ref Button _closeMenu) {
        _closeMenu.onClick.AddListener(delegate { Destroy(gameObject); });
    }
    void editorListener(ref Button _editor) {
        _editor.onClick.AddListener(
            delegate
            {
                Screen.ChangeTo(Screen.ScreenType.EDITOR);
            });
    }

}
