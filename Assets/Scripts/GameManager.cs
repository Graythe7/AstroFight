using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Boss boss;
    public Background backGround;
    public ParticleSystem playerExplosion;

    private int playerLives = 3;
    private int bossHealth = 100; 

    public HealthBar healthBar;
    public Button retryButton;
    public TextMeshProUGUI winText;
    public GameObject gameOverText;
    public GameObject enemySpawner;
    public GameObject bossShoot; // maybe this is extra
    public Image[] playerLivesImg;

    public void Awake(){
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        playerLives = 3;
        bossHealth = 10; // to change back

        healthBar.HealthUI(bossHealth);
        backGround.meshRenderer.material.mainTextureOffset = Vector2.zero;

        player.GetComponent<SpriteRenderer>().enabled = true;
        boss.BossDead(false);

        gameOverText.SetActive(false);
        retryButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        boss.gameObject.SetActive(true);
        bossShoot.gameObject.SetActive(true);
        enemySpawner.gameObject.SetActive(true);


        GameObject[] miniEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");
        foreach (GameObject enemy in miniEnemies)
        {
            Destroy(enemy);
        }

        //the player live images on top left corner 
        for(int i =0; i < 3; i++)
        {
            playerLivesImg[i].enabled = true;
        }

        player.transform.position = new Vector2(-6f, 0.0f);
        boss.transform.position = new Vector2(5f, 0f);

       // SceneManager.LoadScene("Level-1"); 

    }


    public void PlayerDamage()
    {
        if (player.isInvincible) return; //if isInvincible flag is true the rest of method is temporary disable

        playerLives--;

        if (playerLives >= 0 && playerLives < playerLivesImg.Length) // to prevent out of range index error
        {
            playerLivesImg[playerLives].enabled = false;
        }

        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(player.Invincibility());
        }

    }

    public void BossDamage()
    {
        bossHealth--;
        healthBar.HealthUI(bossHealth);

        StartCoroutine(boss.DamageColor());

        if(bossHealth <= 0)
        {
            boss.BossDead(true);
            WinGame();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        playerExplosion.Play();
        player.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 0;

        gameOverText.SetActive(true);
        retryButton.gameObject.SetActive(true);
        //player.gameObject.SetActive(false);
    }

    public void WinGame()
    {
        winText.gameObject.SetActive(true);
        bossShoot.gameObject.SetActive(false); 
        enemySpawner.gameObject.SetActive(false); 

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

}
