using System.Collections;
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
    private bool movingUpTransition = false;
    public bool phase2newGame = false;
  

    private void Awake()
    {
        phase2newGame = false;

        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canMove) //canMove is checked if boss is paused 
        {
            Debug.Log("DefaultMovement");
            DefaultMovement();
        }

        if (movingDownL3) //moving downward at the end of level-3 phase1 
        {
            Debug.Log("movingDownL3");
            MoveDownAtEndOfPhase();

        }
        if (movingUpTransition) //move upward with new animation 
        {
            Debug.Log("movingUpTransition");
            MoveUpTransition();
        }

    }

    private void DefaultMovement()
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

    private void MoveDownAtEndOfPhase()
    {
        
        if (transform.position.y >= -8)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (0.5f * Time.deltaTime), // Moves down slowly
                transform.position.z
            );

            if (transform.position.y <= -6.5f)
            {
                bossAnimator.SetBool("phase2Active", true);
                speed = 1.5f;
                movingDownL3 = false;
                movingUpTransition = true; // to trigger the transition 
            }
        }
    }

    private void MoveUpTransition()
    {
        if (transform.position.y <= 1f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + (1.5f * Time.deltaTime), // Moves down slowly
                transform.position.z
            );
            if(transform.position.y >= 0f)
            {
                canMove = true;
                movingDownL3 = false;
                movingUpTransition = false;
                phase2newGame = true;
                DefaultMovement();
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

    public void MovementPause(bool value) //used in level-2 shoot 
    {
        canMove = value;
    }

    public void Level3Phase1End(bool value)
    {
        movingDownL3 = value;
    }

    

    
}
