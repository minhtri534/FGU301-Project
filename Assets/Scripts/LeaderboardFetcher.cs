using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;
    public int levelCompleted;
    public int totalCoins;
    public int playTimeSeconds;
    public string completedAt;
}

public class LeaderboardFetcher : MonoBehaviour
{
    public void FetchLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    private IEnumerator GetLeaderboard()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:5164/api/progress/leaderboard");

        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = "{\"entries\":" + request.downloadHandler.text + "}";
            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
            foreach (var entry in wrapper.entries)
            {
                Debug.Log($"🏆 {entry.playerName}: {entry.score} pts - Level {entry.levelCompleted}");
            }
        }
        else
        {
            Debug.LogError("❌ Failed to fetch leaderboard: " + request.error);
        }
    }


    [Serializable]
    private class LeaderboardWrapper
    {
        public List<LeaderboardEntry> entries;
    }
}
