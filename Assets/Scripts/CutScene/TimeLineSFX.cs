using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineSFX : MonoBehaviour
{
    AudioManager audioManager;
    public AudioClip ufoAbduction;
    public AudioClip cageOpen;
    public AudioClip Cheer;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void UfoSFX()
    {
        audioManager.PlaySFX(ufoAbduction, 1f);
    }

    public void CageOpenSFX()
    {
        audioManager.PlaySFX(cageOpen, 1f);
    }

    public void CheerSFX()
    {
        audioManager.PlaySFX(Cheer, 1f);
    }

}
