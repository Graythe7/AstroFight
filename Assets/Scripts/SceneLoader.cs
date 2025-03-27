using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1.0f;

    public void LoadScene()
    {
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
            SceneManager.LoadScene(levelIndex);
        }

    }
}
