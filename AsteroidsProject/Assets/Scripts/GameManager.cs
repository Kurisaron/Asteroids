using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // VARIABLES
    private bool gameActive;

    private PlayerData playerData;

    [SerializeField]
    private GameObject largeAsteroidPrefab;
    private int asteroidsActive;

    // FUNCTIONS
    public override void Awake()
    {
        base.Awake();

        gameActive = false;
        playerData = PlayerData.Instance;
    }

    public void SetupGame()
    {
        // Spawn asteroids randomly in the scene
        int startingAsteroids = 1 + (playerData.Level * 2);
        for (int i = 0; i < startingAsteroids; i++)
        {

        }
    }
}
