using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public KeyCode[] keys;
    private int[] mouseButtons = { 0, 1, 2 };
    private KeyBindings keyBindings;

    void Start()
    {
        keyBindings = new KeyBindings(
            new Action[] // KeyboardBindings
            {
                () => Debug.Log("W Key"),
                () => Debug.Log("A Key"),
                () => Debug.Log("S Key"),
                () => Debug.Log("D Key"),
            }, keys,
            new Action[] // MouseBindings
            {
                () => Debug.Log("Left Click"),
                () => Debug.Log("Right Click"),
                () => Debug.Log("Middle Click"),
            }, mouseButtons
        );
    }

    void Update()
    {
        keyBindings.CallBindings();
    }
}
