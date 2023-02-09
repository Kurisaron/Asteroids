using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : SpaceShip
{
    // VARIABLES
    private bool isThrusting;
    private float turnDirection;
    private float turnSpeed = 200.0f;

    // FUNCTIONS
    protected override void Awake()
    {
        base.Awake();

        shootDirection = new Func<Vector3>(() => transform.forward);

        deathAction = GetDeathAction(DeathActionType.Player);
        deathActionSet = true;

        turnDirection = 0.0f;
        speed = 3.0f;
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

        if (deathActionSet && ((collider.gameObject.tag == "Bullet" && collider.gameObject.GetComponent<Bullet>().deathTags.Contains("Player")) || collider.gameObject.tag == "Asteroid" || collider.gameObject.tag == "Saucer"))
        {
            Debug.Log("Player hit");
            deathAction();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isThrusting) transform.position += transform.forward * speed * Time.deltaTime;
        if (turnDirection != 0) transform.Rotate(0, turnDirection * turnSpeed * Time.deltaTime, 0);
    }

    public void Thrust(InputAction.CallbackContext context)
    {
        if (context.started) isThrusting = true;
        if (context.canceled) isThrusting = false;
    }

    public void Turn(InputAction.CallbackContext context)
    {
        turnDirection = context.ReadValue<float>();
        Debug.Log("Turn made, direction: " + turnDirection.ToString());
    }

    public void ShootEvent(InputAction.CallbackContext context)
    {
        if (context.canceled) return;

        Shoot();
    }

    public void Warp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // TO-DO: Do warp
        }
    }
}
