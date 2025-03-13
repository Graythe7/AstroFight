using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator bossAnimator;
    private Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public GameManager gameManager;
   
    public float speed = 1.0f;
    public float minY = -1.5f;
    public float maxY = 1.5f;
    private bool movingUp = true;
    private bool canMove = true;
    private bool movingDownL3 = false;
  

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (movingUp)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.up);

                if (transform.position.y >= maxY)
                {
                    movingUp = false;
                }
            }
            else if (!movingUp)
            {
                transform.Translate(speed * Time.deltaTime * Vector2.down);

                if (transform.position.y <= minY)
                {
                    movingUp = true;
                }
            }
        }

        if (movingDownL3 && transform.position.y >= -6) //moving downward at the end of level-3 phase1 
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (0.5f * Time.deltaTime), // Moves down at 2 units/sec
                transform.position.z
            );
        }

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

    public void BossDead(bool isBossDead)
    {
        bossAnimator.SetBool("isBossDead", isBossDead);
    }

    public void MovementPause(bool value) //used in level-2 shoot 
    {
        canMove = value;
    }

    public void Level3Phase1End(bool value)
    {
        movingDownL3 = value;
    }


    
}
