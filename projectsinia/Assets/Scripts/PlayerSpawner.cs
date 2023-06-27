using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerSetter playerSetter;
    //[SerializeField] private Transform spawnPoint;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = gameObject.transform.position;
        player.name = "Player";
        playerSetter.SetPlayerCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
