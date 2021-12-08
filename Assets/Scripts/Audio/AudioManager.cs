using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : GenericSingletonClass<AudioManager>
{
    [SerializeField] private AudioMixer masterMixer;


    void Start()
    {

    }
    public void SetSound(float soundLevel)
    {
        masterMixer.SetFloat("masterVolume", soundLevel);
    }
    public void SetMusicVolume(float soundLevel)
    {
        masterMixer.SetFloat("musicVolume", soundLevel);
    }

    public void SetSoundEffectVolume(float soundLevel)
    {
        masterMixer.SetFloat("effectVolume", soundLevel);
    }

}
