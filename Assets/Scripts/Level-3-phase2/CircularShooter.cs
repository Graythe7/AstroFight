using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShooter : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform firePoint;     
    public float rotationSpeed = 180f; 
    public float shootInterval = 0.01f; 
    //public float fullCircleTime = 2f;  
    private float angleRotated = 0f;   

    private bool isShooting = true;    // Controls whether the shooter is firing bullets

    void Start()
    {
        angleRotated = 0f;
        Invoke(nameof(StartShooting), 1f);
    }

    void Update()
    {
        if (isShooting)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationStep); // Rotate around its own axis
            angleRotated += rotationStep;

            //ShootBullets();

            // Stop shooting and destroy after completing a full circle (360 degrees)
            if (angleRotated >= 360f)
            {
                isShooting = false;
                StopCoroutine(ShootBullets()); // Stop shooting
                Destroy(gameObject, 0.5f); // Destroy after a short delay
            }
        }
    }

    private void StartShooting()
    {
        StartCoroutine(ShootBullets());
    }

    private IEnumerator ShootBullets()
    {
        while(true)
        {
            Debug.Log("Shooting Starts");

            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            Vector2 shootDirection = transform.right; // Right vector points in the object's facing direction
            bullet.Project(shootDirection);

            yield return new WaitForSeconds(shootInterval);
        }
    }
}

