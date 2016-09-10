using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIGameEnd : MonoBehaviour {

    private GameObject playAgain;
    private GameObject characterSelect;
    private GameObject menu;

    // Use this for initialization
    void Start () {
        playAgain = transform.FindChild("PlayAgain").gameObject;
        characterSelect = transform.FindChild("CharacterSelection").gameObject;
        menu = transform.FindChild("Menu").gameObject;

        Button playAgainButton = playAgain.GetComponent<Button>();
        playAgainListener(ref playAgainButton);

        Button characterSelectButton = characterSelect.GetComponent<Button>();
        characterSelectListener(ref characterSelectButton);

        Button menuButton = menu.GetComponent<Button>();
        menuListener(ref menuButton);
    }

    void playAgainListener(ref Button _playAgain)
    {
        //_playAgain.onClick.AddListener(delegate { /*Refresh "screen_game" with 2 profiles*/});
    }

    void characterSelectListener(ref Button _charSelect)
    {
        //_charSelect.onClick.AddListener(delegate { /*Go to character choice screen*/});
    }

    void menuListener(ref Button _menuSelect)
    {
        _menuSelect.onClick.AddListener(delegate { Application.Quit(); } );
    }



}
