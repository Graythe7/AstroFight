using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlane : MonoBehaviour
{
    public Transform firePoint;
    public Bullet bulletPrefab;

    public float speed = 5f;
    private float lifeTime = 10f;
    private bool moveForward = true;
    private bool movementPause = false;

    public float minAngle = -30f;
    public float maxAngle = 30f;

    private void Start()
    {
        moveForward = true;
        movementPause = false;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (moveForward && !movementPause) // Move forward
        {
            Debug.Log("Moving Forward");
            transform.position = new Vector3(
                transform.position.x - (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
            if(gameObject.transform.position.x <= 0f)
            {
                StartCoroutine(MovementPause());
                moveForward = false;
                
            }
        }
        else if(!moveForward && !movementPause)// Move backward when triggered
        {
            Debug.Log("Moving Backward");
            transform.position = new Vector3(
                transform.position.x + (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
        }

    }

    private void Shoot()
    {
        float[] angles = { minAngle, 0, maxAngle };

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
            bullet.Project(rotation * Vector2.left);
        }
    }

    private IEnumerator MovementPause()
    {
        movementPause = true; // Stop movement
        yield return new WaitForSeconds(0.5f); // Wait for 1 seconds
        Shoot();
        movementPause = false; // Resume movement
    }


}
