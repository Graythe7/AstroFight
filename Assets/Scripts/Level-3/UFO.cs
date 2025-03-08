using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject UFOlights;
    private Player player;
    public float speed = 2.5f;

    private void Start()
    {
        UFOlights.SetActive(false);
        player = FindObjectOfType<Player>();
    }


    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);

        if(player.transform.position.x <= UFOlights.transform.position.x)
        {
           // UFOlights.SetActive(true);
        }
    }
}
