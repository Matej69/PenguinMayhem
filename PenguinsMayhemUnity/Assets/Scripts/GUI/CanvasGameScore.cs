using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CanvasGameScore : MonoBehaviour {

    public List<GameObject> CharacterCards;
    Timer destroyTimer;

	// Use this for initialization
	void Start () {
        destroyTimer = new Timer(3);
        SetCards();
        SetRoundsLeft();
    }
	
	// Update is called once per frame
	void Update () {
        destroyTimer.Tick(Time.deltaTime);
        if (destroyTimer.IsFinished())
            Destroy(gameObject);
	
	}

    void SetCards(){
        for(int i = 0; i < Profile.s_profiles.Count; ++i) {
            if (i >= CharacterCards.Count)
                return;
            Profile profile = Profile.s_profiles[i];
            CharacterCards[i].transform.FindChild("Name").GetComponent<Text>().text = profile.name;
            CharacterCards[i].transform.FindChild("Score").GetComponent<Text>().text = profile.score.ToString();
            CharacterCards[i].transform.FindChild("ImageHat").GetComponent<Image>().sprite = profile.hatSprite;
        }

    }
    void SetRoundsLeft(){
        //ScreenGame.roundsLeft
        transform.FindChild("RoundsNum").GetComponent<Text>().text += ScreenGame.roundsLeft.ToString();
    }

}
