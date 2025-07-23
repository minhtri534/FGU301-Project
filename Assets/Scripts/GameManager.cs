using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWinUI;
    private bool isGameOver = false;
    private bool isGameWin = false;
    void Start()
    {
        UpdateScore();
        gameOverUI.SetActive(false);
        gameWinUI.SetActive(false);
    }

    void Update()
    {

    }

    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }

    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverUI.SetActive(true);

        SaveGameAndSubmitScore(false); 
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUI.SetActive(true);

        SaveGameAndSubmitScore(true); 
    }


    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene("Level 2");
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    
    public bool IsGameWin()
    {
        return isGameWin;
    }

    private void SaveGameAndSubmitScore(bool win)
    {
        int playTime = (int)Time.timeSinceLevelLoad;
        string playerName = "Player1"; 
        int level = win ? 1 : 0;
        int coins = 0; 

        var saver = FindObjectOfType<GameSaver>();
        if (saver != null)
        {
            saver.SaveGame(playerName, level, coins, score, playTime);
        }

        var fetcher = FindObjectOfType<LeaderboardFetcher>();
        if (fetcher != null)
        {
            ScoreSubmission submission = new ScoreSubmission
            {
                playerName = playerName,
                score = score,
                levelCompleted = level,
                totalCoins = coins,
                playTimeSeconds = playTime
            };
            fetcher.SubmitScore(submission);
        }
    }

}
