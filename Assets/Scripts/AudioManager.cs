using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip Shoot;

    public void Play(AudioClip clip, float volume) //play sfxSound
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
    }
}
