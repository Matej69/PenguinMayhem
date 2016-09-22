using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProfileDesign : MonoBehaviour {

    [HideInInspector]
    public Profile profile;
    public GameObject Hat;
    private int curHatIndex = 0;

    //access to GUI elements
    public GameObject InputName;        //profile name
    public GameObject buttonLeft;       //hat switching L/R
    public GameObject buttonRight;
    public GameObject buttonKeyLeft;    //key config
    public GameObject buttonKeyRight;
    public GameObject buttonKeyJump;
    public GameObject buttonKeyShot;
    public GameObject buttonKeyPickup;
    public GameObject buttonReady;      //ready button

    private Dictionary<Profile.keyType, GameObject> buttonKeyTypes = new Dictionary<Profile.keyType, GameObject>();
    private GameObject configButtonPointer = null;
    private bool isConfiguring = false; 


    // Use this for initialization
    void Start () {
        //holding 2 refrences, 'profile' as ref. for this class, and static list ref for later use
        profile = new Profile();
        Profile.s_profiles.Add(profile);
        //set hat sprite to be nohatSprite
        SetPenguinStartHatSprite();        
        //hat switch listeners
        buttonLeftListener();
        buttonRightListener();
        //ready button listener
        ReadyButtonListener();
        //key config button listeners
        ConfigButtonListeners();


    }
	
	// Update is called once per frame
	void Update () {
        CheckForKeyConfig();
	}


    //***********HAT SWITCH BUTTON LISTENERS************
    public void buttonLeftListener() {
        Button button = buttonLeft.GetComponent<Button>();
        button.onClick.AddListener(
            delegate {
                curHatIndex--;                
                List <Sprite> hats = ResourceReader.GetHatSprites();
                curHatIndex = (curHatIndex < 0) ? hats.Count - 1 : curHatIndex;
                Hat.GetComponent<Image>().sprite = hats[curHatIndex % hats.Count];                             
            });
    }
    public void buttonRightListener()
    {
        Button button = buttonRight.GetComponent<Button>();
        button.onClick.AddListener(
            delegate {
                curHatIndex++;
                List<Sprite> hats = ResourceReader.GetHatSprites();
                Hat.GetComponent<Image>().sprite = hats[curHatIndex % hats.Count];                
            });
    }
    //*************BUTTON KEY CONFIG LISTENERS                     
    void ConfigButtonListeners()
    {
        buttonKeyTypes[Profile.keyType.LEFT] = buttonKeyLeft;
        buttonKeyTypes[Profile.keyType.RIGHT] = buttonKeyRight;
        buttonKeyTypes[Profile.keyType.JUMP] = buttonKeyJump;
        buttonKeyTypes[Profile.keyType.SHOT] = buttonKeyShot;
        buttonKeyTypes[Profile.keyType.PICKUP] = buttonKeyPickup;

        foreach(KeyValuePair<Profile.keyType,GameObject> pair in buttonKeyTypes) {
            Button button = pair.Value.GetComponent<Button>();
            button.onClick.AddListener(
              delegate {
                  isConfiguring = true;
                  button.colors.normalColor.Equals(new Color(1,0,1));
                  //configButtonPointer = pair.Value;
                  configButtonPointer = button.transform.gameObject;
              });
        }
        
    }
    //*************BUTTON READY LISTENER
    void ReadyButtonListener()
    {        
        Button button = buttonReady.GetComponent<Button>();
        button.onClick.AddListener(
           delegate {
               if (!profile.isProfileReady) {
                   SetProfileKeyConfig();
                   profile.SetNameFromInput(InputName);                   
                   profile.isProfileReady = true;
                   profile.hatSprite = Hat.GetComponent<Image>().sprite;
                   button.image.color = new Color(0.1f, 1, 0.35f);
               }
               else{                   
                   profile.isProfileReady = false;
                   button.image.color = new Color(1, 1, 1);
               }
           });

    }     
    //************SET ACTION TO KEY CODE IF NEEDED***********
    void CheckForKeyConfig()
    {
        if(configButtonPointer != null && isConfiguring == true) {
            foreach (KeyValuePair<KeyCode, bool> keyPair in InputManager.keyPressed) {
                if (keyPair.Value == true) {
                    configButtonPointer.transform.FindChild("Text").gameObject.GetComponent<Text>().text = keyPair.Key.ToString();
                    isConfiguring = false;
                }
            }   
        }
    }
    //************starting hat sprite and index***************
    void SetPenguinStartHatSprite()
    {
        List<Sprite> hats = ResourceReader.GetHatSprites();
        foreach(Sprite hat in hats) { 
            if(hat.name == "nohat") {                
                Hat.GetComponent<Image>().sprite = Resources.Load<Sprite>(FilePaths.spriteNohat);
                return;      
            }
            curHatIndex++;
        }
    }
    //************GET DICTIONARY OF CONFIG KEYS****************
    public void SetProfileKeyConfig()
    {
        foreach(KeyValuePair<Profile.keyType, GameObject> typeObjPair in buttonKeyTypes) {
            string btnObjText = typeObjPair.Value.transform.FindChild("Text").gameObject.GetComponent<Text>().text;
            foreach(KeyValuePair<KeyCode,bool> keycodeBoolPair in InputManager.keyPressed) {
                if(keycodeBoolPair.Key.ToString() == btnObjText) {
                    profile.keyConfigMap[typeObjPair.Key] = keycodeBoolPair.Key;
                }
            }
        }
    }
}
