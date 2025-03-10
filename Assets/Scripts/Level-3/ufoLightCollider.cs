using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufoLightCollider : MonoBehaviour
{
    public BoxCollider2D lightCollider;

    private void Start()
    {
        lightCollider = GetComponent<BoxCollider2D>();

        lightCollider.enabled = false;

        Invoke(nameof(colliderActive), 0.5f);
    }

    private void colliderActive()
    {
        lightCollider.enabled = true;
    }
}
