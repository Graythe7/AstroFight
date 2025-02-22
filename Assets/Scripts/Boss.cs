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

    
}
