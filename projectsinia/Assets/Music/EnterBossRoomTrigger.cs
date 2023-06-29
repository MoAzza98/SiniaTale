using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossRoomTrigger : MonoBehaviour
{
    AudioManager audioManager;
    // Start is called before the first frame update
    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();

    }
   

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            
            audioManager.OnPlayerEnteredBossRoom();
        }
    }
}
