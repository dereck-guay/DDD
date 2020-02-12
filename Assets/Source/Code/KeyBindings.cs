using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings
{
    private KeyCode[] keys;
    private Action[] keybindings;

    public KeyCode[] Keys
    {
        get { return keys; }
        set { keys = value; }
    }

    public Action[] Keybindings
    {
        get { return keybindings; }
        set { keybindings = value; }
    }

    public KeyBindings(Action[] keybindings, KeyCode[] keys)
    {
        Keybindings = keybindings;

        Keys = keys;
    }

    public void CallBindings()
    {
        /**
         * Les keybindings sont sur onPress faque si le bindings n'a pas de cooldown
         * ça shoot non-stop.
         */

        for (byte i = 0; i < Keys.Length; ++i)
            if (Input.GetKey(Keys[i])) keybindings[i]();
    }
}

/** INSTANTIATE
 * new KeyBindings(
 *      new Action[] {
 *          Fonction utilisées par les keybindings selon l'index.
 *      }, 
 *      new KeyCode[] {
 *          Les keycodes contiennes les keys du clavier et les mouseButton.
 *      }
 * );
 */