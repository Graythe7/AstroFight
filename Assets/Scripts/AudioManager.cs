using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip backGround;
    public AudioClip Shoot;
    public AudioClip GameOver;
    public AudioClip damage;
    /*
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music playing across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate AudioManagers
        }
    }
    */

    private void Start()
    {
        Invoke(nameof(PlayDelay), 1f);
    }

    public void Play(AudioClip clip, float volume) //play sfxSound
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
    }

    private void PlayDelay()
    {
        musicSource.clip = backGround;
        musicSource.Play();
    }

}
