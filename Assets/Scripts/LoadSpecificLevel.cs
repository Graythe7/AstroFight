using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificLevel : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void MainMenu()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        SceneManager.LoadScene(0);
    }

    public void Level_1()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        SceneManager.LoadScene(2);
    }

    public void Level_2()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        SceneManager.LoadScene(3);
    }

    public void Level_3()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        SceneManager.LoadScene(4);
    }

}
