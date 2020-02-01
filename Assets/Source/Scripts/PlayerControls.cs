using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject fireball;
    public LayerMask rayCastHitLayers;

    public KeyCode[] keys;
    private int[] mouseButtons = { 0, 1, 2 };
    private KeyBindings keyBindings;
    private Action[] actions;


    // For demo ONLY
    private float fireballCooldown = 1f;
    private float fireballCurrentCooldown = 0;
    private bool fireballCasted = false;

    void Awake()
    {
        keyBindings = new KeyBindings(
            new Action[] // KeyboardBindings
            {
                () => Move(Vector3.forward),
                () => Move(Vector3.left),
                () => Move(Vector3.back),
                () => Move(Vector3.right)
            }, keys,
            new Action[] // MouseBindings
            {
                () => {
                    if (! fireballCasted)
                        CastFireball();
                },
                () => Debug.Log("Right Click"),
                () => Debug.Log("Middle Click")
            }, mouseButtons
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
        {
            fireballCurrentCooldown += Time.deltaTime;
        }
    }
        

    private Vector3 GetMouseDirection() =>
        (GetMousePositionOn2DPlane() - transform.position).normalized;

    private Vector3 GetMousePositionOn2DPlane()
    {
        var position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayCastHitLayers))
            position = hit.point;

        return position;
    }

    private void Move(Vector3 translation) =>
        transform.Translate(translation * Time.deltaTime * movementSpeed);

    private void CastFireball()
    {
        // OffSet needed to create the fireball out of the playerObject,
        // so that it doesn't collide with it right away.
        var offSet = 1.05f;

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
