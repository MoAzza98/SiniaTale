using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float lifespan;
    private float spawnTime;
    private SpriteRenderer spriteRenderer;
    private Color[] rainbowColors = new Color[7] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.red }; // A list of colors to create a rainbow spectrum

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float Lifespan
    {
        get { return lifespan; }
    }

    public float SpawnTime
    {
        get { return spawnTime; }
        set { spawnTime = value; }
    }

    public void SetLifespan(float lifespan)
    {
        this.lifespan = lifespan;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    void Update()
    {
        if (Time.time - spawnTime > lifespan)
        {
            Destroy(gameObject);
        }
    }
}