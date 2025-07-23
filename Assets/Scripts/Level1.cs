using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject MusicPlayer;
    void Start()
    {
        MusicPlayer = GameObject.Find("MusicPlayer");
        MusicPlayer.GetComponent<MusicPlayer>().PlayTrack(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
