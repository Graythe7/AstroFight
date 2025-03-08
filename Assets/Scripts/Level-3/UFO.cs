using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject UFOlights;
    private GameObject player;
    public float speed = 2.5f;

    private void Start()
    {
        UFOlights.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);

        if(player.transform.position.x + 2 >= UFOlights.transform.position.x)
        {
            Debug.Log("player's postition" + player.transform.position.x);
            UFOlights.SetActive(true);
        }
    }
}
