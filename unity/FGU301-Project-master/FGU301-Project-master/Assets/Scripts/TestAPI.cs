using UnityEngine;

public class TestAPI : MonoBehaviour
{
    void Start()
    {
        var saver = FindFirstObjectByType<GameSaver>();
        if (saver != null)
            saver.SaveGame("TestUnity", 2, 30, 1200, 350);

        var fetcher = FindFirstObjectByType<LeaderboardFetcher>();
        if (fetcher != null)
            fetcher.FetchLeaderboard();
    }
}
