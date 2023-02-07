using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathActionType
{
    Player,
    MediumAsteroid,
    LargeAsteroid,
    Saucer,
    Other
}

public class Destructible : MonoBehaviour
{
    // VARIABLES
    protected float speed;
    protected Vector2 direction;
    protected float wrapOffset = 10.0f;
    protected bool hasDied;
    protected Action deathAction;
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
            case DeathActionType.MediumAsteroid:
                return new Action(() =>
                {
                    if (!hasDied) BreakAsteroid(false);
                });
            case Death
            case DeathActionType.LargeAsteroid:
                return new Action(() => BreakAsteroid(true));
            case DeathActionType.Saucer:
                return new Action(() => ExplodeSaucer());
            case DeathActionType.Other:
                return new Action(() => Destroy(gameObject));
            default:
                return new Action(() => Debug.Log("DeathActionType invalid"));
        }
    }

    protected void ExplodePlayer()
    {

    }

    protected void BreakAsteroid(bool isLarge)
    {
        Debug.Log("Asteroid broken");

        GameObject asteroidPiece = Instantiate(isLarge ? mediumAsteroidPrefab : smallAsteroidPrefab, transform.position + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)), Quaternion.identity);
        Vector2 pieceDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        if (isLarge) asteroidPiece.GetComponent<MediumAsteroid>().direction = pieceDirection;
        else asteroidPiece.GetComponent<SmallAsteroid>().direction = pieceDirection;

        asteroidPiece = Instantiate(isLarge ? mediumAsteroidPrefab : smallAsteroidPrefab, transform.position + new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f, UnityEngine.Random.Range(-1.0f, 1.0f)), Quaternion.identity);
        pieceDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        if (isLarge) asteroidPiece.GetComponent<MediumAsteroid>().direction = pieceDirection;
        else asteroidPiece.GetComponent<SmallAsteroid>().direction = pieceDirection;

        Destroy(gameObject);
    }

    protected void ExplodeSaucer()
    {
        // To-Do: Effect for explosion

        Destroy(gameObject);
    }
}
