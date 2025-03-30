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
    private int BossL3Phase = 1;

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

    AudioManager audioManager;

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

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //audioManager.StartInvoke();
    }

    public void NewGame()
    {
        Time.timeScale = 1;

        StartCoroutine(DelayBeforeFight());

        playerLives = 3;
        bossHealth = 100; // change it back !!  

        WinState = false;
        BossL3Phase = 1;

        healthBar.HealthUI(bossHealth);
        backGround.meshRenderer.material.mainTextureOffset = Vector2.zero;

        player.GetComponent<SpriteRenderer>().enabled = true;
        boss.BossDead(false);
        boss.ActivateBoxCollider(true);

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

        player.transform.position = new Vector2(-5.5f, 0.0f);
        boss.transform.position = new Vector2(4.5f, 0f);

    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        audioManager.PlaySFX(audioManager.buttonUI, 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayerDamage()
    {
        if (player.isInvincible) return; //if isInvincible flag is true the rest of method is temporary disable

        audioManager.PlaySFX(audioManager.damage, 1f);
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

            if(SceneManager.GetActiveScene().name == "Level-3" && BossL3Phase == 2)
            {
                boss.BossPhase2Dead(true);
            }

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
        audioManager.PlaySFX(audioManager.GameOver, 1.3f);
        player.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 0;

        gameOverText.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    
    public void WinGame()
    {
        bossShoot.gameObject.SetActive(false);
        SpawnerActive(false);

        WinState = true;

        if (SceneManager.GetActiveScene().name == "Level-3" && BossL3Phase == 1)
        {
            boss.MovementPause(false);
            boss.Level3Phase1End(true);

            if (boss.phase2newGame)
            {
                NewGameL3Phase2();
            }

        }else if (SceneManager.GetActiveScene().name == "Level-3" && BossL3Phase == 2)
        {
            if (IsSceneLoaded("Level-3-Phase2"))
            {
                SceneManager.UnloadSceneAsync("Level-3-Phase2");
            }

            //InvokeRepeating(nameof(BossParticleEffect), 0f, 1.5f);
            winText.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            boss.enabled = false;
        }
        else
        {
            InvokeRepeating(nameof(BossParticleEffect), 0f, 1.5f);
            winText.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            boss.enabled = false;
        }
 
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

    // Helper function to check if a scene is already loaded
    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true; // Scene is already loaded
            }
        }
        return false; // Scene is not loaded
    }


    public void NewGameL3Phase2()
    {
        Time.timeScale = 1;

        playerLives = 3;
        bossHealth = 100; //dont't forget to change back !!!

        WinState = false;
        BossL3Phase = 2;

        healthBar.HealthUI(bossHealth);

        player.GetComponent<SpriteRenderer>().enabled = true;

        boss.BossDead(false);
        boss.Level3Phase1End(false); //it turns the movingDownL3 = false in boss script
        boss.MovementPause(true);


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
        for (int i = 0; i < 3; i++)
        {
            playerLivesImg[i].enabled = true;
        }

        if (!IsSceneLoaded("Level-3-Phase2"))
        {
            SceneManager.LoadScene("Level-3-Phase2", LoadSceneMode.Additive);
        }


    }

}
