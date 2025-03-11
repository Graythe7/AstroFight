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
        Invoke(nameof(StartSpawning), 3.0f); 
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEB());
    }

    private IEnumerator SpawnEB()
    {
        while (true)
        {
            float curY = Random.Range(minY, maxY);
            Vector2 spawnPosition = new Vector2(8, curY); // pick spawn position for each prefab

            Instantiate(electricBall, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

}
