using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : OrientedMonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject fireball;
    public LayerMask rayCastHitLayers;
    public LayerMask selectableEntities; // Entities that can be selected with mouse click.

    public KeyCode[] keys;
    private KeyBindings keyBindings;

    // For demo ONLY
    private float fireballCooldown = 2f;
    private float fireballCurrentCooldown = 0;
    private bool fireballCasted = false;

    void Awake()
    {
        keyBindings = new KeyBindings(
            new Action[] // KeyboardBindings
            {
                ()=> {
                    if (! fireballCasted)
                        CastFireball();
                },
                () => Debug.Log("Wassup"),
                // Click Bindings
                () => {
                    var playerSelected = GetEntityAtMousePosition();
                    if (playerSelected.activeSelf)
                        Debug.Log(playerSelected.name);
                    else
                        Destroy(playerSelected);
                },
                () => Debug.Log("Left Click"),
                () => Debug.Log("Middle Click")
            }, keys
        );
    }
    
    void Update()
    {
        keyBindings.CallBindings();

        if (fireballCurrentCooldown >= fireballCooldown)
        {
            fireballCasted = false;
            fireballCurrentCooldown = 0f;
        }
        else
            fireballCurrentCooldown += Time.deltaTime;
    }
       

    private void Move(Vector3 direction) =>
        transform.Translate(direction * Time.deltaTime * movementSpeed);

    private void CastFireball()
    {
        // OffSet needed to create the fireball out of the playerObject,
        // so that it doesn't collide with it right away.
        var offSet = 2f;

        var fireballObject = Instantiate(
            fireball,
            transform.position + offSet * GetMouseDirection(),
            Quaternion.identity
        );

        fireballObject
            .GetComponent<MoveTowardsDirection>()
            .direction = GetMouseDirection();

        fireballCasted = true;
    }
}
