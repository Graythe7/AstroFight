using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneChanger : MonoBehaviour
   {
        public float transitionTime;

        private void Start()
        {
            StartCoroutine(ChangeScene(SceneManager.GetActiveScene().buildIndex + 1));
        }

        IEnumerator ChangeScene(int levelIndex)
        {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(levelIndex);
        }
    }

