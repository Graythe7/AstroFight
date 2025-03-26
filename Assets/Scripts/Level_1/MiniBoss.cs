using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public float speed = 15.0f;
    public float lifeTime = 7.0f;
    private float miniBossHealth = 3f;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        miniBossHealth = 3f;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            miniBossHealth--;
            if(miniBossHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
