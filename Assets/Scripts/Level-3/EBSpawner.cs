using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBSpawner : MonoBehaviour
{
    public GameObject electricBall;
    private float minY = -3.0f;
    private float maxY = 3.0f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEB), 3.0f, 1.5f); 
    }

    private void SpawnEB()
    {
        float curY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(8, curY); // pick spawn position for each prefab

        Instantiate(electricBall, spawnPosition, Quaternion.identity);
    }

}
