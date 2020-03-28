using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMenuComponent : MonoBehaviour
{
    private GameObject[] keybindButtons;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text temporary = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        temporary.text = code.ToString();

        
    }
}