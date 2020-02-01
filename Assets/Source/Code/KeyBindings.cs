using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings
{
    // Attr
    private KeyCode[] keys;
    private int[] mouseButtons;
    private Action[] keyboardBindings;
    private Action[] mouseBindings;

    // Props
    public KeyCode[] Keys
    {
        get { return keys; }
        set { keys = value; }
    }

    public int[] MouseButtons
    {
        get { return mouseButtons; }
        set { mouseButtons = value; }
    }

    public Action[] KeyboardBindings
    {
        get { return keyboardBindings; }
        set { keyboardBindings = value; }
    }

    public Action[] MouseBindings
    {
        get { return mouseBindings; }
        set { mouseBindings = value; }
    }

    // Ctor
    public KeyBindings(
        Action[] keyboardBindings,
        KeyCode[] keys,
        Action[] mouseBindings,
        int[] mouseButtons
    )
    {
        KeyboardBindings = keyboardBindings;
        MouseBindings = mouseBindings;

        Keys = keys;
        MouseButtons = mouseButtons;
    }

    // Methods
    public void CallBindings()
    {
        for (byte i = 0; i < Keys.Length; ++i)
            if (Input.GetKey(Keys[i])) KeyboardBindings[i]();

        for (byte i = 0; i < MouseButtons.Length; ++i)
            if (Input.GetMouseButtonDown(MouseButtons[i])) MouseBindings[i]();
    }
}
