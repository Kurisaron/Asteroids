using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : SpaceShip
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        Vector3 temp = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        shootDirection = new Func<Vector3>(() => temp);
        StartCoroutine(ShootRoutine());

        deathAction = GetDeathAction(DeathActionType.Saucer);
        deathActionSet = true;

        direction = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (deathActionSet && collider.gameObject.tag == "Bullet" && collider.gameObject.GetComponent<Bullet>().deathTags.Contains("Saucer"))
        {
            deathAction();
        }
    }

    protected override void Update()
    {
        base.Update();

        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
    }

    public IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            
            yield return new WaitForSeconds(2f);
        }
    }
}
