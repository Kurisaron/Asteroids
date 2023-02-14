using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Destructible
{
    // VARIABLES
    public BulletSource bulletSource;
    public GameObject bulletPrefab;
    protected Func<Vector3> shootDirection;

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    protected void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (shootDirection() * 0.5f), Quaternion.identity);
        bullet.transform.forward = shootDirection();
        bullet.GetComponent<Bullet>().SetSource(bulletSource);
        bullet.GetComponentInChildren<Renderer>().material.color = bulletSource == BulletSource.Player ? Color.yellow : Color.red;
        bullet.GetComponent<Bullet>().Shoot();
    }
}
