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

        speed = 5.0f;
    }
}
