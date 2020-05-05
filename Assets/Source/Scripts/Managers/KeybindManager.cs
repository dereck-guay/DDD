using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    private static KeybindManager instance;

    public static KeybindManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<KeybindManager>();

            return instance;
        }
    }

    public Dictionary<string, KeyCode> KeyBinds { get; private set; }
    public Dictionary<string, KeyCode> ActionBinds { get; private set; }
    private string bindName;

    void Start()
    {
        KeyBinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();

        // Movements.
        BindKey("UP", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("UP", "W")));
        BindKey("LEFT", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LEFT", "A")));
        BindKey("DOWN", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DOWN", "S")));
        BindKey("RIGHT", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RIGHT", "D")));

        // Spells.
        BindKey("ACTAA", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACTAA", "Mouse0"))); // AutoAttack.
        BindKey("ACT1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACT1", "Alpha1")));
        BindKey("ACT2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACT2", "Alpha2")));
        BindKey("ACT3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACT3", "Alpha3")));
        BindKey("ACT4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ACT4", "Alpha4")));
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        var currentDictionary = KeyBinds;

        if (key.Contains("ACT"))
            currentDictionary = ActionBinds;

        // Set new keyBind
        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            UIManagerComponent.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            var ValueKey = currentDictionary.FirstOrDefault(pair => pair.Value == keyBind).Key;
            currentDictionary[ValueKey] = KeyCode.None; // UnBind.
            UIManagerComponent.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind; // Binds key.
        UIManagerComponent.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }

    public void KeyBindOnClick(string bindName)
    {
        this.bindName = bindName;
    }

    private void OnGUI()
    {
        if (bindName != string.Empty)
        {
            Event e = Event.current;

            if(e.isKey || e.isMouse)
            {
                if (e.type == EventType.MouseDown)
                {
                    if (e.button == 0)
                    {
                        BindKey(bindName, KeyCode.Mouse0);
                    }

                    if (e.button == 1)
                    {
                        BindKey(bindName, KeyCode.Mouse1);
                    }

                    if (e.button == 2)
                    {
                        BindKey(bindName, KeyCode.Mouse2);
                    }

                    if (e.button == 3)
                    {
                        BindKey(bindName, KeyCode.Mouse3);
                    }

                    if (e.button == 4)
                    {
                        BindKey(bindName, KeyCode.Mouse4);
                    }

                    if (e.button == 5)
                    {
                        BindKey(bindName, KeyCode.Mouse5);
                    }

                    if (e.button == 6)
                    {
                        BindKey(bindName, KeyCode.Mouse6);
                    }
                }
                    
                BindKey(bindName, e.keyCode);
            }
        }
    }

    public void SaveKeys()
    {
        foreach (var key in KeyBinds)
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        
        foreach (var actKey in ActionBinds)
            PlayerPrefs.SetString(actKey.Key, actKey.Value.ToString());
        
        PlayerPrefs.Save();
    }
}
