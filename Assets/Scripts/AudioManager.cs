using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    private AudioClip currentMusicClip; // Stores the current playing music

    public AudioClip backGround;
    public AudioClip Shoot;
    public AudioClip GameOver;
    public AudioClip damage;
    
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
    

    private void Start()
    {
        Invoke(nameof(PlayDefaultMusic), 1f);
    }

    public void PlaySFX(AudioClip clip, float volume) //play sfxSound
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        Debug.Log("StopMusic called");
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            currentMusicClip = null; // Reset current track

        }
    }

    private void PlayDefaultMusic()
    {
        StartMusic(backGround);
    }

    public void StartMusic(AudioClip clip)
    {
        if (clip == null) return; // Avoid playing null clips

        // Only change the music if it's a new track
        if (currentMusicClip != clip)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
            currentMusicClip = clip;
        }

    }
   
}
