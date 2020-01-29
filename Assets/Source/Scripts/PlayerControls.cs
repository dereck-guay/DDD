using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject pointer;
    public int planeLayer = 8;

    public KeyCode[] keys;
    private int[] mouseButtons = { 0, 1, 2 };
    private KeyBindings keyBindings;
    private Action[] actions;

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
            new Action[3] // MouseBindings
            {
                () => Debug.Log("Left Click"),
                () => Debug.Log("Right Click"),
                () => Debug.Log("Middle Click")
            }, mouseButtons
        );
    }
    
    void Update()
    {
        keyBindings.CallBindings();

        pointer.transform.position = GetMousePositionOn2DPlane();
    }

    public Vector3 GetMousePositionOn2DPlane()
    {
        var position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            position = hit.point;

        return position;
    }

    public void Move(Vector3 translation) =>
        transform.Translate(translation * Time.deltaTime * movementSpeed);
}
