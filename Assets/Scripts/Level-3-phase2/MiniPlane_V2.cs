using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlane_V2 : MonoBehaviour
{
    public Animator miniPlaneV2Animator;
    public Transform firePoint;
    public Bullet bulletPrefab;

    public float speed = 3f;
    private float lifeTime = 10f;
    private bool moveForward = true;
    private bool movementPause = false;

    private float minX = 0f;
    private float maxX = 4f;
    private float randomX;

    private void Start()
    {
        moveForward = true;
        movementPause = false;

        randomX = Random.Range(minX, maxX);
        Debug.Log("randomX:" + randomX);

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (moveForward && !movementPause) // Move forward
        {
            transform.position = new Vector3(
                transform.position.x - (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );


            if (gameObject.transform.position.x <= randomX)
            {
                StartCoroutine(MovementPause());
                moveForward = false;

            }
        }
        else if (!moveForward && !movementPause)// Move backward when triggered
        {
            miniPlaneV2Animator.SetBool("isFlipped", true);

            transform.position = new Vector3(
                transform.position.x + (speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
        }

    }

    private void Shoot()
    {
        float[] angles = { -60,-30, 0, 30, 60 };

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
