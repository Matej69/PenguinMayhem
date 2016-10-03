using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SoundOptionsGlobal {

    [HideInInspector]
    public static float masterVolume;

    static SoundOptionsGlobal()
    {
        masterVolume = 100;
    }
    
    public static void SetVolume(float _volume)
    {
        masterVolume = _volume;
    }

}

public class SoundInfo
{
    //static
    public static Dictionary<WeaponInfo.weaponType, SoundInfo> allSoundInfos = new Dictionary<WeaponInfo.weaponType,SoundInfo>();
    static SoundInfo()
    {
       allSoundInfos[WeaponInfo.weaponType.REVOLVER]        =   new SoundInfo("Revolver", 1);
       allSoundInfos[WeaponInfo.weaponType.AK47]            =   new SoundInfo("ak47", 0.55f);
       allSoundInfos[WeaponInfo.weaponType.SHOTGUN]         =   new SoundInfo("Shotgun", 1.5f);
       allSoundInfos[WeaponInfo.weaponType.SPACEPISTOL]     =   new SoundInfo("SpaceGun", 1);
       allSoundInfos[WeaponInfo.weaponType.UZI]             =   new SoundInfo("Uzi", 1);
       allSoundInfos[WeaponInfo.weaponType.GRENADE]         =   new SoundInfo("Explosion", 0.2f);
    }

    //non-static
    public AudioClip audio;
    public float volume;
    public SoundInfo(string fileName, float _volume) {
        audio = Resources.Load<AudioClip>(FilePaths.soundFolder+fileName);
        volume = _volume;
    }
}
