using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlaneSpawner : MonoBehaviour
{
    public GameObject miniPlanePrefab;

    private float maxY = 3f;
    private float minY = -3f;
    private float minSpawnTime = 4f;
    private float maxSpawnTime = 7f;

    private void Start()
    {
        Invoke(nameof(StartSpawning), 3f);
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
            Instantiate(miniPlanePrefab, spawnPosition, Quaternion.identity);

            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }


}
