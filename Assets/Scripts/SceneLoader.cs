using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    AudioManager audioManager;

    public float transitionTime = 1.0f;

    // Dictionary to store background music for each level
    public List<AudioClip> levelMusicList; // Assign clips in Inspector

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        PlayLevelMusic(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        audioManager.StopMusic();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if(SceneManager.GetActiveScene().name == "Level-3")
        {
            SceneManager.LoadScene(levelIndex + 1);
        }
        else
        {
            PlayLevelMusic(levelIndex);
            SceneManager.LoadScene(levelIndex);
        }

    }

    private void PlayLevelMusic(int levelIndex)
    {
        if (audioManager != null && levelIndex < levelMusicList.Count)
        {
            AudioClip selectedMusic = levelMusicList[levelIndex];

            // Only play if it's a new track (to prevent unnecessary restarts)
            if (audioManager.backGround != selectedMusic)
            {
                audioManager.StartMusic(selectedMusic);
            }
        }
    }
}
