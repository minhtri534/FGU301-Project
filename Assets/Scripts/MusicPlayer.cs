using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    
    private List<AudioResource> Songs = new();
    private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Songs = new List<AudioResource>
        {
            Resources.Load<AudioResource>("Song that might play in a platformer menu v2"),
            Resources.Load<AudioResource>("Song that might play in a platformer"),
            Resources.Load<AudioResource>("Song that might play in a platformer as well")
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayTrack(int trackNumber)
    {
        /*
        foreach (AudioResource song in Songs)
        {
            Debug.Log(song.name);
        }
        audio.Stop();
        audio.resource = Songs[trackNumber];
        audio.Play();
        */
    }

    public void PausePlaying()
    {
        audio.Pause();
    }

    public void UnPausePlaying()
    {
        audio.UnPause();
    }
}
