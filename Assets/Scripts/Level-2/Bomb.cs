using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;
    public float lifeTime = 7f;
    public float playerTargetX;
    private bool reachedTargetX = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        playerTargetX = player.transform.position.x;
        
        if (reachedTargetX == false)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (Mathf.Abs(transform.position.x - playerTargetX) < 0.1f) // Small tolerance to avoid precision issues
            {
                reachedTargetX = true;
            }
        }
        else if (reachedTargetX == true)
        {
            Destroy(gameObject);
            CreateSpikes();
        }
    }

    private void CreateSpikes()
    {

    }

}
