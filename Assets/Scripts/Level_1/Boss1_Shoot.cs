using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Shoot : MonoBehaviour
{
    public Transform firePoint;
    public Bullet bulletPrefab;
    public GameManager gameManager;

    private bool canShoot = false;

    // Variables for shooting intervals
    private float nextShootTime = 0f; // Time when the next shot can occur
    private float minFireRate = 1f; // Minimum interval between shots
    private float maxFireRate = 2f; // Maximum interval between shots
    public float minAngle = -30f;
    public float maxAngle = 30f;


    private void Start()
    {
        StartCoroutine(StartShootingDelay());
    }

    private IEnumerator StartShootingDelay()
    {
        yield return new WaitForSeconds(3.5f); //initial 3.5 second delay 
        canShoot = true;
        nextShootTime = Time.time + Random.Range(minFireRate, maxFireRate);
    }

    private void Update()
    {
        // Shooting logic with random intervals
        if (canShoot && Time.time > nextShootTime) // Check if the current time has passed the next shoot time
        {
            Shoot();
            nextShootTime = Time.time + Random.Range(minFireRate, maxFireRate); // Set the next shoot time
        }
    }

    public void Shoot()
    {
        if(gameManager.WinState == false)
        {
            float[] angles = { minAngle, 0, maxAngle };

            foreach (float angle in angles)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Bullet bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                bullet.Project(rotation * Vector2.left);
            }
        }
        
    }
}
