using System.Collections;
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
    public ParticleSystem bossExplosion;

    private int playerLives = 3;
    public int bossHealth = 100; 
    public bool WinState = false;

    public HealthBar healthBar;
    public Button retryButton;
    public Button nextLevelButton;
    public TextMeshProUGUI winText;
    public GameObject gameOverText;
    public GameObject[] enemySpawner; 
    public GameObject bossShoot; 
    public GameObject ReadyTxt;
    public GameObject FightTxt;
    public Image[] playerLivesImg;

    public void Awake(){
        NewGame();
    }

    private IEnumerator DelayBeforeFight()
    {
        
        ReadyTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ReadyTxt.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        FightTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        FightTxt.gameObject.SetActive(false);

    }

    public void NewGame()
    {
        Time.timeScale = 1;

        StartCoroutine(DelayBeforeFight());

        playerLives = 3;
        bossHealth = 100; 

        WinState = false;

        healthBar.HealthUI(bossHealth);
        backGround.meshRenderer.material.mainTextureOffset = Vector2.zero;

        player.GetComponent<SpriteRenderer>().enabled = true;
        boss.BossDead(false);

        gameOverText.SetActive(false);
        retryButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        boss.gameObject.SetActive(true);
        bossShoot.gameObject.SetActive(true);
        
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

        SpawnerActive(true);

        player.transform.position = new Vector2(-6f, 0.0f);
        boss.transform.position = new Vector2(5f, 0f);

    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }

    
    public void WinGame()
    {
        winText.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
        bossShoot.gameObject.SetActive(false);
        SpawnerActive(false);

        boss.enabled = false;

        InvokeRepeating(nameof(BossParticleEffect), 0f, 1.5f);

        WinState = true;
    }

    private void BossParticleEffect()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        Vector3 effectPosition = boss.transform.position + randomOffset;

        bossExplosion.transform.position = effectPosition;
        bossExplosion.Play();
    }

    private void SpawnerActive(bool value)
    {
        for (int i = 0; i < enemySpawner.Length; i++)
        {
            enemySpawner[i].SetActive(value);
        }

    }

   

}
