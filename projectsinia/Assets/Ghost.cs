using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float lifespan;
    private float spawnTime;

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

    void Update()
    {
        if (Time.time - spawnTime > lifespan)
        {
            Destroy(gameObject);
        }
    }
}