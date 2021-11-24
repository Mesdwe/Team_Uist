using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    void Start()
    { //for testing
        SetAudioClip(0);
    }
    public void SetAudioClip(int index)
    {
        audioSource.clip = audioClips[index];
    }

    public void PlayAudioClip()
    {
        audioSource.Play();
    }
}
