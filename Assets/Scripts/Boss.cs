using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Bullet bulletPrefab;
    public Transform firePoint;
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;

    public float speed = 1.0f;
    public float minY = -1.5f;
    public float maxY = 1.5f;
    private bool movingUp = true;

    // Variables for shooting intervals
    private float nextShootTime = 0f; // Time when the next shot can occur
    private float minFireRate = 1f; // Minimum interval between shots
    private float maxFireRate = 2f; // Maximum interval between shots

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (movingUp)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);

            if(transform.position.y >= maxY)
            {
                movingUp = false;
            }
        }else if (!movingUp)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.down);

            if (transform.position.y <= minY)
            {
                movingUp = true;
            }
        }

        // Shooting logic with random intervals
        if (Time.time > nextShootTime) // Check if the current time has passed the next shoot time
        {
            Shoot();
            nextShootTime = Time.time + Random.Range(minFireRate, maxFireRate); // Set the next shoot time
        }

    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.Project(Vector2.left);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")){
            gameManager.BossDamage();
        }
    }

    public IEnumerator DamageColor()
    {
        spriteRenderer.color = new Color(234f / 255f, 204f / 255f, 204f / 255f, 1f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
