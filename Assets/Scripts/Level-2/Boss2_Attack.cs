using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Attack : MonoBehaviour
{
    public Transform firePoint;
    public Bullet bulletPrefab;
    public GameManager gameManager;


    // Variables for shooting intervals
    private float nextShootTime = 0f; // Time when the next shot can occur
    private float minFireRate = 2f; // Minimum interval between shots
    private float maxFireRate = 4f; // Maximum interval between shots


    private void Start()
    {
        StartCoroutine(StartShootingDelay());
        Invoke(nameof(StartShoot), 2.5f);
    }

    private IEnumerator StartShootingDelay()
    {
        yield return new WaitForSeconds(3.5f); //initial 3.5 second delay 
        //canShoot = true;
        nextShootTime = Time.time + Random.Range(minFireRate, maxFireRate);
    }

    private void StartShoot()
    {
         StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        if (gameManager.WinState == false)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 spawnPosition = new Vector2(firePoint.position.x + i, firePoint.position.y);
                Bullet bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.Project(Vector2.left);
            }

            //float spawnDelay = Random.Range(minFireRate, maxFireRate);
            yield return new WaitForSeconds(2f);

        }

    }
}
