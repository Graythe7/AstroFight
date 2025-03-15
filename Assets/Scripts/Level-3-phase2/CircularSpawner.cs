using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
    public GameObject circularShooterPrefab;

    private void Start()
    {
        Invoke(nameof(Spawn), 15f);
    }

    private void Spawn()
    {
        Instantiate(circularShooterPrefab, transform.position, Quaternion.identity);
    }
}
