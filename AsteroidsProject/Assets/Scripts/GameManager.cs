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
    [SerializeField]
    private GameObject smallSaucerPrefab;
    [SerializeField]
    private GameObject bigSaucerPrefab;
    public int asteroidsActive;
    public int saucersActive;

    // FUNCTIONS
    public override void Awake()
    {
        base.Awake();

        gameActive = false;
    }

    public void Start()
    {
        playerData = PlayerData.Instance;
        StartGame();
    }

    private void StartGame()
    {
        playerData.level = 1;

        SetupGame(true);
    }

    public void SetupGame(bool fromStart)
    {
        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        playerData.gameObject.transform.position = new Vector3(origin.x, 0, origin.z);
        playerData.gameObject.transform.rotation = Quaternion.identity;
        
        if (fromStart)
        {
            // Spawn asteroids randomly in the scene
            int startingAsteroids = 1 + (playerData.level * 2);
            for (int i = 0; i < startingAsteroids; i++)
            {
                Vector3 randomPoint = RandomPoint();
                Instantiate(largeAsteroidPrefab, new Vector3(randomPoint.x, 0, randomPoint.z), Quaternion.identity);
                asteroidsActive += 1;
            }

        }

        gameActive = true;
        //StartCoroutine(SpawnSaucer());
    }

    public void RestartGame()
    {
        gameActive = false;
        
        PlayerControl.local.deathAction = null;
        PlayerControl.local.deathAction = GetDeathAction(DeathActionType.Player);

        SetupGame(false);
    }

    public void EndGame()
    {
        gameActive = false;

        // Destroy all asteroids, saucers, and bullets
        

        UIManager.Instance.EnableEndScreen();
    }

    public void NextLevel()
    {
        if (PlayerData.Instance.level >= 3)
        {
            EndGame();
        }
        else
        {
            PlayerData.Instance.level += 1;
            Debug.Log("Level: " + PlayerData.Instance.level.ToString());
            SetupGame(true);
        }
    }

    public IEnumerator SpawnSaucer()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(10.0f);

            Vector3 randomPoint = RandomPoint();

            if (Random.Range(0, 5) == 0)
            {
                Instantiate(smallSaucerPrefab, new Vector3(randomPoint.x, 0, randomPoint.z), Quaternion.identity);
            }
            else
            {
                Instantiate(bigSaucerPrefab, new Vector3(randomPoint.x, 0, randomPoint.z), Quaternion.identity);
            }

            saucersActive += 1;
        }
    }

    public void CheckForNoEnemies()
    {
        if (gameActive && asteroidsActive <= 0 && saucersActive <= 0)
        {
            NextLevel();
        }
    }

    public Vector3 RandomPoint()
    {
        float distanceFromOrigin = 1.0f;
        Vector3 randomPoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0.0f, Screen.width), Random.Range(0.0f, Screen.height)));

        if (Mathf.Abs(randomPoint.x) < distanceFromOrigin) randomPoint.x += Mathf.Sign(randomPoint.x) * distanceFromOrigin;
        if (Mathf.Abs(randomPoint.z) < distanceFromOrigin) randomPoint.z += Mathf.Sign(randomPoint.z) * distanceFromOrigin;

        return randomPoint;
    }
}
