using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button StartButton;
    public Button SelectLevelButton;
    public GameObject LevelsUI;
    public GameObject Title;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void SelectLevelPage()
    {
        audioManager.PlaySFX(audioManager.buttonUI, 1f);

        Title.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        SelectLevelButton.gameObject.SetActive(false);

        LevelsUI.gameObject.SetActive(true);
    }

}
