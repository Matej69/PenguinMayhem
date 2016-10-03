using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager soundManagerPointer;

	// Use this for initialization
	void Start () {
        soundManagerPointer = gameObject.GetComponent<SoundManager>();

    }
	
	// Update is called once per frame
	void Update () {
        

    }

    public void PlaySound(WeaponInfo.weaponType soundType) {
        GetComponent<AudioSource>().clip = SoundInfo.allSoundInfos[soundType].audio;
        GetComponent<AudioSource>().PlayOneShot(SoundInfo.allSoundInfos[soundType].audio, SoundInfo.allSoundInfos[soundType].volume * (SoundOptionsGlobal.masterVolume/100));
    }

}
