using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public GameManager gameManager;
    private Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
 
    public float speed = 5f;
    private Vector2 direction;
    public bool isInvincible = false;
    public float invincibilityDuration = 3.0f;
    public float flashInterval = 0.5f;
    public float fireRate = 2.0f; // interval between continious shooting
    private float nextFireTime = 0.0f; 


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector2(horizontal, vertical).normalized;

    }


    private void FixedUpdate()
    {
        rigidBody.AddForce(direction * speed);


        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = fireRate + Time.time; 
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(gameManager.WinState == false)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("MiniEnemy") || other.gameObject.CompareTag("EnemyBullet"))
            {
                gameManager.PlayerDamage();
            }
        }
        
    }

    public IEnumerator Invincibility()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        //this method use elapsedTime(passedTime) to decide how long to run
        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // flickering effect
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

}