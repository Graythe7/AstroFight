using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float speed = 500.0f;
    public float lifetime = 5.0f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rigidBody.AddForce(direction * speed);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
