using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSaucer : Saucer
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        deathAction = GetDeathAction(DeathActionType.BigSaucer);
        deathActionSet = true;

        speed = 5.0f;
        shootInterval = 4.0f;
    }
}
