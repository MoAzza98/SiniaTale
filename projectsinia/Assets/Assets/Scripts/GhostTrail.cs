using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab; // the ghost prefab to use for the trail
    public float spawnInterval; // how often to spawn a new ghost
    public float lifespan; // how long each ghost should last
    public int maxGhosts; // how many ghosts to show at once
    public float distanceThreshold; // minimum distance the sprite must move before spawning additional ghosts
    public float trailDuration; // how long the ghost trail should last
    private float trailTimer; // timer for the ghost trail
    public bool ghostTrailOn;

    private SpriteRenderer sr;
    private float timeSinceLastSpawn = 0.0f;
    private List<GameObject> ghosts = new List<GameObject>();
    private Vector3 lastGhostPosition;
    private Queue<GameObject> ghostPool = new Queue<GameObject>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lastGhostPosition = transform.position;
    }

    void Update()
    {
        if (trailTimer > 0.0f)
        {
            trailTimer -= Time.deltaTime;

            // calculate the distance the sprite has moved since the last ghost spawn
            float distanceMoved = Vector3.Distance(transform.position, lastGhostPosition);

            // spawn a new ghost if enough time has elapsed or the sprite has moved far enough
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval || distanceMoved >= distanceThreshold)
            {
                timeSinceLastSpawn -= spawnInterval;
                SpawnGhost();
                lastGhostPosition = transform.position;
            }

            // remove old ghosts that have exceeded their lifespan
            while (ghosts.Count > 0 && Time.time - ghosts[0].GetComponent<Ghost>().SpawnTime > lifespan)
            {
                GameObject oldGhost = ghosts[0];
                ghosts.RemoveAt(0);
                ReturnToPool(oldGhost);
            }
        }
        else
        {
            // if the trail timer has expired, destroy all ghosts and reset the timer
            foreach (GameObject ghost in ghosts)
            {
                ReturnToPool(ghost);
            }
            ghosts.Clear();
            trailTimer = 0.0f;
        }
    }

    void SpawnGhost()
    {
        // get a ghost from the pool or create a new one if the pool is empty
        GameObject newGhost;
        if (ghostPool.Count > 0)
        {
            newGhost = ghostPool.Dequeue();
            newGhost.SetActive(true);
        }
        else
        {
            newGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        }

        // add the ghost to the list and set its position and rotation
        ghosts.Add(newGhost);
        newGhost.transform.position = transform.position;
        newGhost.transform.rotation = transform.rotation;

        // set the ghost's lifespan and spawn time
        Ghost ghost = newGhost.GetComponent<Ghost>();
        if (ghost != null)
        {
            ghost.SetLifespan(lifespan);
            ghost.SpawnTime = Time.time;
        }

        // deactivate any excess ghosts beyond the maximum allowed
        while (ghosts.Count > maxGhosts)
        {
            GameObject oldGhost = ghosts[0];
            ghosts.RemoveAt(0);
            ReturnToPool(oldGhost);
        }

        newGhost.transform.localScale = new Vector2(transform.parent.localScale.x, transform.parent.localScale.y);

        // set the color of the ghost based on the index of the ghost in the ghost trail
        int ghostIndex = ghosts.Count - 1;
        int rainbowColorIndex = ghostIndex % 7;
        Color color = Color.HSVToRGB((float)rainbowColorIndex / 7f, 1f, 1f);
        ghost.SetColor(color);
        ghost.SetSprite(sr.sprite);
    }

    void ReturnToPool(GameObject ghost)
    {
        // check if the ghost is still active before returning it to the pool
        if (ghost.activeSelf)
        {
            ghost.SetActive(false);
            ghostPool.Enqueue(ghost);
        }
    }

    public void StartTrail(float time)
    {
        trailTimer = time;
    }
}