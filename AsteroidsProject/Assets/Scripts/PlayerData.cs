using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    // PROPERTIES
    public int Level { get; private set; }

    // FUNCTIONS
    public override void Awake()
    {
        base.Awake();

        Level = 1;
    }
}
