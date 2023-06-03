using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab; // the ghost prefab to use for the trail
    public float spawnInterval = 0.1f; // how often to spawn a new ghost
    public float lifespan = 1.0f; // how long each ghost should last
    public int maxGhosts = 10; // how many ghosts to show at once
    public float distanceThreshold = 0.1f; // minimum distance the sprite must move before spawning additional ghosts

    private float timeSinceLastSpawn = 0.0f;
    private LinkedList<GameObject> ghosts = new LinkedList<GameObject>();
    private Vector3 lastGhostPosition;
    private Queue<GameObject> ghostPool = new Queue<GameObject>();

    void Start()
    {
        lastGhostPosition = transform.position;
    }

    void Update()
    {
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
        while (ghosts.Count > 0 && Time.time - ghosts.First.Value.GetComponent<Ghost>().SpawnTime > lifespan)
        {
            GameObject oldGhost = ghosts.First.Value;
            ghosts.RemoveFirst();
            ReturnToPool(oldGhost);
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

        // add the ghost to the linked list and set its position and rotation
        ghosts.AddLast(newGhost);
        newGhost.transform.position = transform.position;
        newGhost.transform.rotation = transform.rotation;

        // set the ghost's lifespan and spawn time
        Ghost ghost = newGhost.GetComponent<Ghost>();
        if (ghost != null)
        {
            ghost.SetLifespan(lifespan);
            ghost.SpawnTime = Time.time;
        }

        // fill in any gaps in the ghost trail by reactivating inactive ghosts within the lifespan of the trail
        foreach (GameObject ghostObj in ghosts)
        {
            if (!ghostObj.activeSelf && Time.time - ghostObj.GetComponent<Ghost>().SpawnTime <= lifespan)
            {
                ghostObj.SetActive(true);
            }
        }

        // deactivate any excess ghosts beyond the maximum allowed
        while (ghosts.Count > maxGhosts)
        {
            GameObject oldGhost = ghosts.First.Value;
            ghosts.RemoveFirst();
            ReturnToPool(oldGhost);
        }
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
}