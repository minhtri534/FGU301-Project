using UnityEngine;

public class Level2 : MonoBehaviour
{
public GameObject MusicPlayer;
    void Start()
    {
        MusicPlayer = GameObject.Find("MusicPlayer");
        MusicPlayer.GetComponent<MusicPlayer>().PlayTrack(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
