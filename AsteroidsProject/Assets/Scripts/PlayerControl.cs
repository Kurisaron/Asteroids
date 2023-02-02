using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : SpaceShip
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        shootDirection = new Func<Vector3>(() => transform.forward);

        deathAction = GetDeathAction(DeathActionType.Player);
        deathActionSet = true;


    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (deathActionSet && collider.gameObject.tag == "Bullet" && collider.gameObject.GetComponent<Bullet>().deathTags.Contains("Player"))
        {
            deathAction();
        }
    }

    protected override void Update()
    {
        base.Update();

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
