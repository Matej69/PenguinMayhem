using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIGameEnd : MonoBehaviour {

    private GameObject playAgain;
    private GameObject characterSelect;
    private GameObject mainMenu;
    private GameObject closeMenu;

    // Use this for initialization
    void Start () {
        playAgain = transform.FindChild("PlayAgain").gameObject;
        characterSelect = transform.FindChild("CharacterSelection").gameObject;
        mainMenu = transform.FindChild("Menu").gameObject;
        closeMenu = transform.FindChild("CloseMenu").gameObject;

        Button playAgainButton = playAgain.GetComponent<Button>();
        playAgainListener(ref playAgainButton);

        Button characterSelectButton = characterSelect.GetComponent<Button>();
        characterSelectListener(ref characterSelectButton);

        Button menuButton = mainMenu.GetComponent<Button>();
        menuListener(ref menuButton);

        Button closeMenuButton = closeMenu.GetComponent<Button>();
        closeMenuListener(ref closeMenuButton);
        
    }

    void playAgainListener(ref Button _playAgain)
    {
        //_playAgain.onClick.AddListener(delegate { /*Refresh "screen_game" with 2 profiles*/});
    }

    void characterSelectListener(ref Button _charSelect)
    {
        //_charSelect.onClick.AddListener(delegate { /*Go to character choice screen*/});
    }

    void menuListener(ref Button _mainMenu)
    {
        _mainMenu.onClick.AddListener(delegate { Application.Quit(); } );
    }

    void closeMenuListener(ref Button _closeMenu)
    {
        _closeMenu.onClick.AddListener(delegate { Destroy(gameObject); });
    }





}
