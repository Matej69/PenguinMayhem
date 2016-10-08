using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    
    [HideInInspector]
    public AudioSource backgroundMusicSource;
    [HideInInspector]
    public AudioSource soundSource;

    public GameObject freeSoundClipPrefab;
    
    enum backMusicState { PLAYING,STOPED,PAUSED };
    backMusicState backgroundMusicState;

    void Awake() {
        backgroundMusicState = backMusicState.STOPED;
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.clip = Resources.Load<AudioClip>(FilePaths.soundFolder + "theme");
        soundSource = gameObject.AddComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
             
    }
	
	// Update is called once per frame
	void Update () {
        ResetBackgroundAudioIfNeeded();
    }

    public void PlaySound(WeaponInfo.weaponType soundType) {
        GameObject soundClipObject = Instantiate(freeSoundClipPrefab);
        soundClipObject.GetComponent<FreeAudioClip>().SetSettings(SoundInfo.allSoundInfos[soundType].audio, SoundInfo.allSoundInfos[soundType].volume * (SoundOptionsGlobal.masterVolume / 100));
    }

    public void PlaySplash() {
        soundSource.clip = SoundInfo.bloodSoundInfo.audio;
        soundSource.PlayOneShot(SoundInfo.bloodSoundInfo.audio, SoundInfo.bloodSoundInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
    }

    public void PlayCheering()
    {
        soundSource.clip = SoundInfo.cheersInfo.audio;
        soundSource.PlayOneShot(SoundInfo.cheersInfo.audio, SoundInfo.cheersInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
    }




    // background music functions
    public void PlayBackgroundMusic() {
        backgroundMusicSource.loop = true;
        if (!backgroundMusicSource.isPlaying) {
            if (backgroundMusicState == backMusicState.STOPED) { 
                backgroundMusicSource.PlayOneShot(backgroundMusicSource.clip, SoundInfo.backgroundSoundInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
                backgroundMusicState = backMusicState.PLAYING;
            }
            else if (backgroundMusicState == backMusicState.PAUSED)
            {
                backgroundMusicSource.UnPause();
                backgroundMusicState = backMusicState.PLAYING;
            }
        }  
    }
    public void PauseBackgroundMusic() {
        if (backgroundMusicSource.isPlaying && backgroundMusicSource != null) { 
            backgroundMusicSource.Pause();
            backgroundMusicState = backMusicState.PAUSED;
        }
    }
    public void StopBackgroundMusic() {
        if (backgroundMusicSource.isPlaying && backgroundMusicSource != null) { 
            backgroundMusicSource.Stop();
            backgroundMusicState = backMusicState.STOPED;
        }
    }

    //set audio pointer to start of audio ==> LOOP
    void ResetBackgroundAudioIfNeeded() {
        if(backgroundMusicState == backMusicState.PLAYING && !backgroundMusicSource.isPlaying)
            backgroundMusicSource.PlayOneShot(backgroundMusicSource.clip, SoundInfo.backgroundSoundInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
    }

}
