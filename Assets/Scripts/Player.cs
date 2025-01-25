using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameManager gameManager;
    private Rigidbody2D rigidBody;

    public float speed = 5f;
    private Vector2 direction;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(direction * speed);
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.PlayerDied();
        }
    }

}
