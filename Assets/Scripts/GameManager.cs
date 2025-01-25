using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    private int playerLives = 3;


    private void NewGame()
    {
        playerLives = 3;
    }


    public void PlayerDied()
    {
        playerLives--;


    }

    public void RespawnPlayer()
    {
        //player.gameManager.transform.position = Vector2(-6.5, 0);
    }

    public void GameOver()
    {
        //to do 
    }
}
