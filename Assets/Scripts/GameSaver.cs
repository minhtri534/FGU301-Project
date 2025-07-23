using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class GameSaver : MonoBehaviour
{
    public void SaveGame(string playerName, int level, int coins, int score, int time)
    {
        PlayerProgressDto progress = new PlayerProgressDto
        {
            playerName = playerName,
            levelCompleted = level,
            totalCoins = coins,
            score = score,
            playTimeSeconds = time
        };

        StartCoroutine(SendProgress(progress));
    }

    private IEnumerator SendProgress(PlayerProgressDto progress)
    {
        string json = JsonUtility.ToJson(progress);

        UnityWebRequest request = new UnityWebRequest("http://localhost:5164/api/progress/save", "POST");
        byte[] body = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("✅ Progress saved successfully!");
        else
            Debug.LogError("❌ Save failed: " + request.error);
    }
}
