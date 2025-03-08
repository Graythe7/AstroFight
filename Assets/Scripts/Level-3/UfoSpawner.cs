using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    public GameObject ufoPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnUfo), 10f, 5f);
    }

    private void SpawnUfo()
    {
        Instantiate(ufoPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
