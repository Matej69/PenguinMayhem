using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Profile {

    public static List<Profile> s_profiles = new List<Profile>();
    
    public enum keyType { LEFT,RIGHT,JUMP,SHOT };
    public Dictionary<keyType, KeyCode> keyConfigMap = new Dictionary<keyType, KeyCode>();

    public string name;
    public int score = 0;

    public GameObject character;
    public Sprite hatSprite = Resources.Load<Sprite>(FilePaths.spriteNohat);    //default sprite will be noHat sprite
    public bool isProfileReady = false;

    public Profile() {
        KeyConfigInitValues();
        //CreateCharacterObject(new Vector2(5, 5));
    }

    public void CreateCharacterObject(Vector2 cord)
    {
        character = (GameObject)MonoBehaviour.Instantiate((GameObject)Resources.Load(FilePaths.characterObj), cord, Quaternion.identity);
        character.GetComponent<CharacterAttributes>().profilePointer = this;
        character.transform.FindChild("Hat").gameObject.GetComponent<SpriteRenderer>().sprite = hatSprite;

    }

    public void SetNameFromInput(GameObject inputObj) {
        string text = inputObj.transform.FindChild("Text").GetComponent<Text>().text;
        if (text != "")
            name = text;
        else
            name = "HotDonkey69";
    }


    public static void ClearProfileList(/*ref List<Profile> profiles*/) {
        foreach(Profile _prof in Profile.s_profiles) {
            _prof.keyConfigMap.Clear();
            if (_prof.character != null)
                MonoBehaviour.Destroy(_prof.character);
        }
        Profile.s_profiles.Clear();
    }   
    public static void RemoveUnreadyProfiles() {
        for(int i = Profile.s_profiles.Count-1; i >= 0; --i) {
            if (!Profile.s_profiles[i].isProfileReady)
                Profile.s_profiles.RemoveAt(i);
        } 
    }


    public void KeyConfigInitValues(){
        keyConfigMap[Profile.keyType.JUMP] = KeyCode.Minus;
        keyConfigMap[Profile.keyType.LEFT] = KeyCode.Minus;
        keyConfigMap[Profile.keyType.RIGHT] = KeyCode.Minus;
        keyConfigMap[Profile.keyType.SHOT] = KeyCode.Minus;
    }

   

}
