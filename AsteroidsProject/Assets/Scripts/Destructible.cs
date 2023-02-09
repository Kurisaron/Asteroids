using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathActionType
{
    Player,
    SmallAsteroid,
    MediumAsteroid,
    LargeAsteroid,
    SmallSaucer,
    BigSaucer,
    Other
}

public enum AsteroidSize
{
    Small,
    Medium,
    Large
}

public enum SaucerSize
{
    Small,
    Big
}

public class Destructible : MonoBehaviour
{
    // VARIABLES
    protected float speed;
    protected Vector2 direction;
    protected float wrapOffset = 10.0f;
    protected bool hasDied;
    public Action deathAction;
    protected bool deathActionSet;
    public GameObject smallAsteroidPrefab, mediumAsteroidPrefab;

    // FUNCTIONS
    protected virtual void Awake()
    {
        deathActionSet = false;
        hasDied = false;
    }

    protected virtual void Update()
    {
        ScreenWrap();
    }

    protected void ScreenWrap()
    {
        Vector3 tempVector = Camera.main.WorldToScreenPoint(transform.position);
        if (Mathf.Abs(tempVector.x - (Screen.width / 2)) > (Screen.width / 2))
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3((tempVector.x - (Screen.width / 2)) > 0 ? wrapOffset : Screen.width - wrapOffset, 0, 0)).x , transform.position.y, transform.position.z);
        }

        if (Mathf.Abs(tempVector.y - (Screen.height / 2)) > (Screen.height / 2))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.ScreenToWorldPoint(new Vector3(0, (tempVector.y - (Screen.height / 2)) > 0 ? wrapOffset : Screen.height - wrapOffset, 0)).z);
        }
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

    }

    
    protected Action GetDeathAction(DeathActionType actionType)
    {
        switch (actionType)
        {
            case DeathActionType.Player:
                return new Action(() =>
                {
                    if (!hasDied) ExplodePlayer();
                });
            case DeathActionType.SmallAsteroid:
                return new Action(() =>
                {
                    if (!hasDied) BreakAsteroid(AsteroidSize.Small);
                });
            case DeathActionType.MediumAsteroid:
                return new Action(() =>
                {
                    if (!hasDied) BreakAsteroid(AsteroidSize.Medium);
                });
            case DeathActionType.LargeAsteroid:
                return new Action(() =>
                {
                    if (!hasDied) BreakAsteroid(AsteroidSize.Large);
                });
            case DeathActionType.SmallSaucer:
                return new Action(() =>
                {
                    if (!hasDied) ExplodeSaucer(SaucerSize.Small);
                });
            case DeathActionType.BigSaucer:
                return new Action(() =>
                {
                    if (!hasDied) ExplodeSaucer(SaucerSize.Big);
                });
            case DeathActionType.Other:
                return new Action(() => Destroy(gameObject));
            default:
                return new Action(() => Debug.Log("DeathActionType invalid"));
        }
    }

    protected void ExplodePlayer()
    {
        hasDied = true;

        // Die and restart
        if (PlayerData.Instance.currentLives > 0)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            GameManager.Instance.EndGame();
        }
    }

    protected void BreakAsteroid(AsteroidSize asteroidSize)
    {
        Debug.Log("Asteroid broken");
        hasDied = true;

        if (asteroidSize != AsteroidSize.Small)
        {
            Instantiate(asteroidSize == AsteroidSize.Large ? mediumAsteroidPrefab : smallAsteroidPrefab, transform.position + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            Instantiate(asteroidSize == AsteroidSize.Large ? mediumAsteroidPrefab : smallAsteroidPrefab, transform.position + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            GameManager.Instance.asteroidsActive += 2;

        }

        switch (asteroidSize)
        {
            case AsteroidSize.Small:
                PlayerData.Instance.AddScore(50);
                break;
            case AsteroidSize.Medium:
                PlayerData.Instance.AddScore(25);
                break;
            case AsteroidSize.Large:
                PlayerData.Instance.AddScore(10);
                break;
            default:
                break;
        }

        GameManager.Instance.asteroidsActive -= 1;
        GameManager.Instance.CheckForNoEnemies();
        Destroy(gameObject);
    }

    protected void ExplodeSaucer(SaucerSize saucerSize)
    {
        hasDied = true;

        switch (saucerSize)
        {
            case SaucerSize.Small:
                PlayerData.Instance.AddScore(150);
                break;
            case SaucerSize.Big:
                PlayerData.Instance.AddScore(75);
                break;
            default:
                break;
        }

        GameManager.Instance.saucersActive -= 1;
        GameManager.Instance.CheckForNoEnemies();
        Destroy(gameObject);
    }
}
