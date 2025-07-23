using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text; 
using TMPro;

[System.Serializable]
public class ScoreSubmission
{
    public string playerName;
    public int levelCompleted;
    public int totalCoins;
    public int score;
    public int playTimeSeconds;
}

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

    void Start()
    {
        FetchLeaderboard();
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

    public void SubmitScore(ScoreSubmission submission)
    {
        StartCoroutine(SendScore(submission));
    }

    private IEnumerator SendScore(ScoreSubmission submission)
    {
        string json = JsonUtility.ToJson(submission);

        UnityWebRequest request = new UnityWebRequest("http://localhost:5164/api/progress/save", "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("✅ Score submitted to leaderboard!");
        else
            Debug.LogError("❌ Leaderboard submission failed: " + request.error);
    }

    [Serializable]
    private class LeaderboardWrapper
    {
        public List<LeaderboardEntry> entries;
    }
}
