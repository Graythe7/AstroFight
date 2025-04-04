using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlaneV2Spawner : MonoBehaviour
{   
    public GameObject miniPlaneV2Prefab;

    private float maxY = 2f;
    private float minY = -2f;
    private float minSpawnTime = 5f;
    private float maxSpawnTime = 9f;
    private float planeHealth = 3f;

    private void Start()
    {
        Invoke(nameof(StartSpawning), 6f);
        planeHealth = 3f;
    }

    private void StartSpawning()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            float curY = Random.Range(minY, maxY);
            Vector2 spawnPosition = new Vector2(8, curY);
            Instantiate(miniPlaneV2Prefab, spawnPosition, Quaternion.identity);

            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            planeHealth--;
            if (planeHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}