using UnityEngine;
using System.Collections;

public class FreeAudioClip : MonoBehaviour {

    public AudioSource audioSource;
    bool playedAtSomePoint = false;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        if (IsReadyToPlay() && !playedAtSomePoint){
            audioSource.PlayOneShot(audioSource.clip, audioSource.volume);
            playedAtSomePoint = true;
        }
        if (playedAtSomePoint && !audioSource.isPlaying)
            Destroy(gameObject);
	
	}


    bool IsReadyToPlay() {
        return (audioSource != null && audioSource.clip != null);
    }

    public void SetSettings(AudioClip _audioClip,float _volume){
        audioSource.clip = _audioClip;
        audioSource.volume = _volume;
    }

}
