using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource audioSource;
    private bool playAudio;

    void Start()
    {
        playAudio = false;
    }

    void Update()
    {
        if (playAudio == true & audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
    }

    public void playMusicOnLoop()
    {
        playAudio = true;
        audioSource.PlayDelayed(2);

    }
}
