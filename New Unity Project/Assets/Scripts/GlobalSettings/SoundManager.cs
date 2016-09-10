using UnityEngine;
using System.Collections;

public static class SoundManager {

    [HideInInspector]
    public static float masterVolume;

    static SoundManager()
    {
        masterVolume = 50;
    }
    
    public static void SetVolume(float _volume)
    {
        masterVolume = _volume;
    }

}
