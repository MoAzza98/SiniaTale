using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float lifespan;
    private float spawnTime;
    private SpriteRenderer spriteRenderer;

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

    void Update()
    {
        if (Time.time - spawnTime > lifespan)
        {
            Destroy(gameObject);
        }
        else
        {
            // calculate the color of the ghost based on a time-based function
            float t = (Time.time - spawnTime) / lifespan;
            Color color = Color.HSVToRGB(Mathf.Sin(t * Mathf.PI * 8) * 0.5f + 0.5f, 1.0f, 1.0f);

            // set the color of the sprite renderer
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
        }
    }
}