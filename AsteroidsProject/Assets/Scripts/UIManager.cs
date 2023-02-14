using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    // VARIABLES
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;

    public GameObject endScreen;
    public Text endTitle;
    public Image endBackground;

    // FUNCTIONS
    public override void Awake()
    {
        base.Awake();

        endScreen.SetActive(false);
    }

    public void Update()
    {
        livesText.text = "Lives: " + PlayerData.Instance.currentLives;
        scoreText.text = "Score: " + PlayerData.Instance.currentScore;
        highScoreText.text = "High Score: " + PlayerData.Instance.highScore;
    }

    public void EnableEndScreen(EndType endType)
    {
        Debug.Log("END SCREEN ENABLED");
        endScreen.SetActive(true);
        endTitle.text = endType == EndType.Win ? "Winner!" : "Game Over";
        endBackground.color = endType == EndType.Win ? new Color(0, 1.0f, 0, 0.5f) : new Color(0, 1.0f, 0, 0.5f);
    }

    public void RetryButtonEvent()
    {
        endScreen.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void QuitButtonEvent()
    {
        Application.Quit();
    }

}
