using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float speed;
    private float amplitude;
    private float frequency;
    private float lifeTime = 5f;
    private Vector2 startPosition;
    public Color color1 = Color.red;
    public Color color2 = Color.blue;
    public Color color3 = Color.cyan;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        speed = Random.Range(10, 15);
        amplitude = Random.Range(0.2f, 0.7f);
        frequency = Random.Range(4, 12);
        spriteRenderer.color = GetRandomColor();

        startPosition = transform.position;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
         
        Vector2 movement = Vector2.left * speed * Time.deltaTime;
        float y = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude; 

        transform.position += new Vector3(movement.x, 0, 0); 
        transform.position = new Vector2(transform.position.x, y); 
    }

    private Color GetRandomColor()
    {
        Color[] colors = { color1, color2, color3 };
        return colors[Random.Range(0, colors.Length)];
    }
    

}
