using UnityEngine;

public class AudioListenerManager : MonoBehaviour
{
    void Awake()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        if (listeners.Length > 1) // If there is more than one AudioListener
        {
            Debug.LogWarning("Multiple Audio Listeners found! Disabling extra ones.");
            for (int i = 1; i < listeners.Length; i++) // Keep one, disable the rest
            {
                listeners[i].enabled = false;
            }
        }
    }
}
