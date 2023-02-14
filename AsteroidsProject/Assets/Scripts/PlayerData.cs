using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    // PROPERTIES
    public int currentLives;
    public int maxLives = 3;
    public int currentScore;
    public int highScore;
    public int level;

    // FUNCTIONS
    public override void Awake()
    {
        base.Awake();

        currentLives = maxLives;
        currentScore = 0;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score is now " + currentScore.ToString());
    }

    public void SaveHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }
}
