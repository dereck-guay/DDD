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
        BindKey("UP", KeyCode.W);
        BindKey("LEFT", KeyCode.A);
        BindKey("DOWN", KeyCode.S);
        BindKey("RIGHT", KeyCode.D);

        // Spells.
        BindKey("ACTAA", KeyCode.Mouse0); // AutoAttack.
        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);
        BindKey("ACT4", KeyCode.Alpha4);
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        var currentDictionary = KeyBinds;

        if (key.Contains("ACT"))
            currentDictionary = ActionBinds;

        // Set new keyBind.
        if (!currentDictionary.ContainsValue(keyBind))
            currentDictionary.Add(key, keyBind);
        else if (currentDictionary.ContainsValue(keyBind))
        {
            var ValueKey = currentDictionary.FirstOrDefault(pair => pair.Value == keyBind).Key;
            currentDictionary[ValueKey] = KeyCode.None; // UnBind.
        }

        currentDictionary[key] = keyBind; // Binds key.
        bindName = string.Empty;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {

    }
}
