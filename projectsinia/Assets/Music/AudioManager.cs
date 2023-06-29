using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip bossMusic;
    private AudioSource audioSource;

    private bool isPlayingBossMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();

        isPlayingBossMusic = false;
    }

    // This function is called whenever the player enters a boss room
    public void OnPlayerEnteredBossRoom()
    {
        // Only play the boss music if it's not already playing
        if (!isPlayingBossMusic)
        {
            // Stop the current music and play the boss music
            audioSource.Stop();
            audioSource.clip = bossMusic;
            audioSource.loop = true;
            audioSource.Play();

            isPlayingBossMusic = true;
        }
    }

    // This function is called whenever the player exits a boss room
    public void OnPlayerExitedBossRoom()
    {
        // Only switch back to the regular music if the boss music is currently playing
        if (isPlayingBossMusic)
        {
            // Stop the boss music and play the regular music
            audioSource.Stop();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();

            isPlayingBossMusic = false;
        }
    }
}
