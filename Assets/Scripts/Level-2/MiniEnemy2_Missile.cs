using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy2_Missile : MonoBehaviour
{
    private GameObject player;
    public Animator animator;
    private float playerTargetX;
    private bool reachedTargetX = false; // to check if it has reach the players x position

    public float speed = 10f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player"); //Cause you can't reference a gameObject in scene in a prefab

        animator = GetComponent<Animator>();

        animator.Play("Missile_idle");

        Destroy(gameObject, 7.0f);
    }

    private void Update()
    { 
        Movement();
    }

    private void Movement()
    {
        playerTargetX = player.transform.position.x;

        if(reachedTargetX == false)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (Mathf.Abs(transform.position.x - playerTargetX) < 0.1f) // Small tolerance to avoid precision issues
            {
                reachedTargetX = true; 
            }
        }
        else if(reachedTargetX == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            transform.Translate(speed * Time.deltaTime * Vector2.left);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
