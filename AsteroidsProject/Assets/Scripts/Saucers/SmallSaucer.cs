using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSaucer : Saucer
{
    // VARIABLES
    private float scoreForMostAccurate = 10000.0f;

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        deathAction = GetDeathAction(DeathActionType.SmallSaucer);
        deathActionSet = true;

        speed = 10.0f;
        shootInterval = 2.0f;

        shootDirection = new Func<Vector3>(() => (PlayerData.Instance.gameObject.transform.position - transform.position).normalized + new Vector3(UnityEngine.Random.Range(-AccuracyDeviance(), AccuracyDeviance()), 0, UnityEngine.Random.Range(-AccuracyDeviance(), AccuracyDeviance())));
    }

    protected override void Update()
    {
        base.Update();
    }

    private float AccuracyDeviance()
    {
        return Mathf.Lerp(1.0f, 0.1f, PlayerData.Instance.currentScore / scoreForMostAccurate);
    }
}
