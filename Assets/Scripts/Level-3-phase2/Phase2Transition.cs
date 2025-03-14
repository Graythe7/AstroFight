using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Phase2Transition : MonoBehaviour
{
    public static Phase2Transition Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the LevelManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadSceneSeamlessly(sceneName));
    }

    private IEnumerator LoadSceneSeamlessly(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false; // Wait before switching

        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // Wait for the scene to load
        }

        asyncLoad.allowSceneActivation = true; // Activate the scene

        yield return new WaitForSeconds(1f); // Short delay before unloading the previous scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}


