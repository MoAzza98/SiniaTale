using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab; // The prefab to use for the ghost trail
    public int numberOfGhosts = 5; // The number of ghosts to generate
    public float ghostSpawnDelay = 0.1f; // The delay between spawning each ghost
    public float ghostLifeTime = 1f; // The lifetime of each ghost

    private List<GameObject> ghosts; // A list to store the generated ghosts

    private void Start()
    {
        ghosts = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        // Spawn a new ghost at the current position of the sprite
        GameObject newGhost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghosts.Add(newGhost);

        // Remove the oldest ghost if the list exceeds the maximum number of ghosts
        if (ghosts.Count > numberOfGhosts)
        {
            GameObject oldestGhost = ghosts[0];
            ghosts.RemoveAt(0);
            Destroy(oldestGhost);
        }

        // Set the lifetime of each ghost
        foreach (GameObject ghost in ghosts)
        {
            StartCoroutine(DestroyGhost(ghost, ghostLifeTime));
        }
    }

    IEnumerator DestroyGhost(GameObject ghost, float delay)
    {
        yield return new WaitForSeconds(delay);
        ghosts.Remove(ghost);
        Destroy(ghost);
    }
}