using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    
    public List<AudioResource> Songs;
    private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayTrack(int trackNumber)
    {
        audio.Stop();
        audio.resource = Songs[trackNumber];
        audio.Play();
    }

    void PausePlaying()
    {
        audio.Pause();
    }

    void UnPausePlaying()
    {
        audio.UnPause();
    }
}
