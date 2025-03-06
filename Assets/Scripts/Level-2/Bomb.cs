using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private GameObject player;
    public Bullet spikePrefab;
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;
    public float spikeSpeed = 1f;
    public float lifeTime = 7f;
    private float playerTargetX;
    private bool reachedTargetX = false;
    private bool spikesSpawned = false;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time *0.5f, 1);
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
        Shoot();
    }

    public void Shoot()
    {
        playerTargetX = player.transform.position.x+1;
        
        if (reachedTargetX == false)
        {
            transform.Translate(speed * Time.deltaTime * Vector2.left);

            if (Mathf.Abs(transform.position.x - playerTargetX) < 0.5f) // Small tolerance to avoid precision issues
            {
                reachedTargetX = true;
            }
        }
        else if (reachedTargetX == true && spikesSpawned == false)
        {
            spriteRenderer.enabled = false;
            CreateSpikes();
            spikesSpawned = true; // cause shoot function is called in Update/ every frame
        }
    }

    private void CreateSpikes()
    {
        float[] angles = new float[] { 0f, 45f, 90f, 135f, 180f,225f, -45f, -90f};

        foreach (float angle in angles)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Bullet bullet = Instantiate(spikePrefab, transform.position, rotation);
            bullet.Project(rotation * Vector2.left);
        }

        spikesSpawned = false;
    }

}
