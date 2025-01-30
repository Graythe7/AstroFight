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

    private int playerLives = 3;
    private int bossHealth = 100;

    public HealthBar healthBar;
    public Button retryButton;
    public TextMeshProUGUI winText;
    public GameObject gameOverText;
    public GameObject enemySpawner;
    public Image[] playerLivesImg;

    public void Awake(){
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        playerLives = 3;
        bossHealth = 100;

        healthBar.HealthUI(bossHealth);
        backGround.meshRenderer.material.mainTextureOffset = Vector2.zero;

        gameOverText.SetActive(false);
        retryButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        boss.gameObject.SetActive(true);
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

        player.transform.position = new Vector2(-6.5f, 0.0f);
        boss.transform.position = new Vector2(5.5f, 0f);

       // SceneManager.LoadScene("Level-1"); 

    }


    public void PlayerDamage()
    {
        if (player.isInvincible) return; //if isInvincible flag is true the rest of method is temporary disable

        playerLives--;
        playerLivesImg[playerLives].enabled = false;

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
            WinGame();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.SetActive(true);
        retryButton.gameObject.SetActive(true);

    }

    public void WinGame()
    {
        winText.gameObject.SetActive(true);
        boss.gameObject.SetActive(false); // change this 
        enemySpawner.gameObject.SetActive(false); 

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

}
