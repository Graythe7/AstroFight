using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject miniBossPrefab;
    public Transform spawnPoint;
    public int numberOfEnemiesToSpawn = 4;
    public float minY = -3.0f;
    public float maxY = 3.0f;
    public float spacing = 1.0f;
    public float minSpawnTime = 3.0f;
    public float maxSpawnTime = 6.0f;
    

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float randomY = Random.Range(minY, maxY);

            for(int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                Vector2 spawnPosition = new Vector2(spawnPoint.position.x + i, randomY);
                Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
            }

            //to delay between each set
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
