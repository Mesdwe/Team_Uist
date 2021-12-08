using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingUI : MonoBehaviour
{
    public void ChangeVolume(float pct)
    {
        AudioManager.Instance.SetSound(pct);
    }

    public void ChangeMusicVolume(float pct)
    {
        AudioManager.Instance.SetMusicVolume(pct);
    }

    public void ChangeEffectVolume(float pct)
    {
        AudioManager.Instance.SetSoundEffectVolume(pct);
    }
}
