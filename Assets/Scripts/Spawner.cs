using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject miniBossPrefab;
    public Transform spawnPoint;
    private Transform player;
    public int numberOfEnemiesToSpawn = 4;
    public float spacing = 1.0f;
    public float minSpawnTime = 3.0f;
    public float maxSpawnTime = 6.0f;
    private float playerTargetY;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke(nameof(StartSpawning), 3f); // add delay before the main attack
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());

    }


    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            playerTargetY = player.position.y;

            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                Vector2 spawnPosition = new Vector2(spawnPoint.position.x + i, playerTargetY);
                Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
            }

            //to delay between each set
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
