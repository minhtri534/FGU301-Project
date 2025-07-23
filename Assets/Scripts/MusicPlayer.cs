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

    public void PlayTrack(int trackNumber)
    {
        audio.Stop();
        audio.resource = Songs[trackNumber];
        audio.Play();
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
