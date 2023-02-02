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

        speed = 10.0f;
    }
}
