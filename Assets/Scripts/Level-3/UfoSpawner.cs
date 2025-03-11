using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    public GameObject ufoPrefab;
    private float spawnDelay;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 2.0f;

    private void Start()
    {
        Invoke(nameof(StartSpawning), 5f);
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnUfo());
    }

    private IEnumerator SpawnUfo()
    {
        while (true)
        {
            Instantiate(ufoPrefab, gameObject.transform.position, Quaternion.identity);
            spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
