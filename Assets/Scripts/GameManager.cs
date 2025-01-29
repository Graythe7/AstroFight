using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    private int playerLives = 3;
 
    public Boss boss;
    private int bossHealth = 100;

    public HealthBar healthBar;
    public GameObject gameOverText;
    public Button retryButton;

    public void Awake(){
        NewGame();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        playerLives = 3;
        bossHealth = 100;

        healthBar.HealthUI(bossHealth);

        gameOverText.SetActive(false);
        retryButton.gameObject.SetActive(false);

        GameObject[] miniEnemies = GameObject.FindGameObjectsWithTag("MiniEnemy");
        foreach (GameObject enemy in miniEnemies)
        {
            Destroy(enemy);
        }

        player.transform.position = new Vector2(-6.5f, 0.0f);
        boss.transform.position = new Vector2(5.5f, 0f);

       // SceneManager.LoadScene("Level-1"); 

    }


    public void PlayerDamage()
    {
        if (player.isInvincible) return; //if isInvincible flag is true the rest of method is temporary disable

        playerLives--;

        if(playerLives <= 0)
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
        //to do
    }

}
