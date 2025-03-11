using UnityEngine;

public class UFO : MonoBehaviour
{
    private GameObject player;
    public GameObject ufoLight;
    public float speed = 2.5f;
    private float lifeTime = 7f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ufoLight.SetActive(false);
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
        if (player.transform.position.x + 0.5f >= transform.position.x || transform.position.x <= -1.5f) //same as equal 
        {
            //Debug.Log("position.x = player");

            ufoLight.SetActive(true);
            
        }
    }

}
