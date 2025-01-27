using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    private int playerLives = 3;
 
    public Boss boss;
    private int bossHealth = 100;


    private void NewGame()
    {
        playerLives = 3;
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

        StartCoroutine(boss.DamageColor());

        if(bossHealth <= 0)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        //to do 
    }

    public void WinGame()
    {
        //to do
    }

}
