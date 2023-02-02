using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Destructible
{
    // VARIABLES

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        direction = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (deathActionSet && collider.gameObject.tag == "Bullet" && collider.gameObject.GetComponent<Bullet>().deathTags.Contains("Asteroid"))
        {
            deathAction();
        }
    }

    protected override void Update()
    {
        base.Update();

        transform.position += new Vector3(direction.x, 0, direction.y) * speed * Time.deltaTime;
    }

}
