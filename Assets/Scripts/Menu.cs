using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MusicPlayer;
    void Start()
    {
        MusicPlayer = GameObject.Find("MusicPlayer");
        MusicPlayer.GetComponent<MusicPlayer>().PlayTrack(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
