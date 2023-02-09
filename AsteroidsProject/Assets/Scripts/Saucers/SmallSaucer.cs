using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSaucer : Saucer
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        deathAction = GetDeathAction(DeathActionType.SmallSaucer);
        deathActionSet = true;

        speed = 10.0f;
    }
}
