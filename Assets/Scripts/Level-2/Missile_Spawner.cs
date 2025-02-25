using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private float minSpawnTime = 7.0f;
    private float maxSpawnTime = 9.0f;

    private void Start()
    {
        Invoke(nameof(StartSpawning), 4.5f);
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
