using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSource
{
    Player,
    Enemy
}

public class Bullet : Destructible
{
    // VARIABLES
    private float lifeTime = 3.0f;
    [HideInInspector]
    public List<string> deathTags = new List<string>();
    private bool canMove = false;

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        deathAction = GetDeathAction(DeathActionType.Other);
        deathActionSet = true;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (deathActionSet && deathTags.Contains(collider.gameObject.tag))
        {
            deathAction();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (canMove)
        {
            transform.position += transform.forward * 10f * Time.deltaTime;
        }
    }

    public void Shoot()
    {
        canMove = true;
        StartCoroutine(DeathCounter());
    }

    public void SetSource(BulletSource bulletSource)
    {
        switch (bulletSource)
        {
            case BulletSource.Player:
                deathTags.Add("Saucer");
                deathTags.Add("Asteroid");
                break;
            case BulletSource.Enemy:
                deathTags.Add("Player");
                break;
            default:
                deathTags.Add("");
                break;
        }
    }

    public IEnumerator DeathCounter()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
