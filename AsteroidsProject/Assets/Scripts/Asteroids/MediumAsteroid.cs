using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumAsteroid : Asteroid
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        deathAction = GetDeathAction(DeathActionType.MediumAsteroid);
        deathActionSet = true;

        speed = 2.25f;
    }
}
