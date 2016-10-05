using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    
    [HideInInspector]
    public AudioSource backgroundMusicSource;
    [HideInInspector]
    public AudioSource soundSource;

    public GameObject freeSoundClipPrefab;

    private bool isPaused = false;
    enum backMusicState { PLAYING,STOPED,PAUSED };

    void Awake() {
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.loop = true;
        soundSource = gameObject.AddComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {   
            
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void PlaySound(WeaponInfo.weaponType soundType) {
        GameObject soundClipObject = Instantiate(freeSoundClipPrefab);
        soundClipObject.GetComponent<FreeAudioClip>().SetSettings(SoundInfo.allSoundInfos[soundType].audio, SoundInfo.allSoundInfos[soundType].volume * (SoundOptionsGlobal.masterVolume / 100));
    }

    public void PlaySplash()
    {
        soundSource.clip = SoundInfo.bloodSoundInfo.audio;
        soundSource.PlayOneShot(SoundInfo.bloodSoundInfo.audio, SoundInfo.bloodSoundInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
    }



    // background music functions
    public void PlayBackgroundMusic() {
        if (!backgroundMusicSource.isPlaying) {
            if (!isPaused) { 
                backgroundMusicSource.PlayOneShot(SoundInfo.backgroundSoundInfo.audio, SoundInfo.backgroundSoundInfo.volume * (SoundOptionsGlobal.masterVolume / 100));
                isPaused = false;
            }
            else if (isPaused)
            {
                backgroundMusicSource.UnPause();
                isPaused = false;
            }
        }  
    }
    public void PauseBackgroundMusic() {
        if (backgroundMusicSource.isPlaying && backgroundMusicSource != null) { 
            backgroundMusicSource.Pause();
            isPaused = true;
        }
    }
    public void StopBackgroundMusic() {
        if (backgroundMusicSource.isPlaying && backgroundMusicSource != null) { 
            backgroundMusicSource.Stop();
            isPaused = false;
        }
    }



}
