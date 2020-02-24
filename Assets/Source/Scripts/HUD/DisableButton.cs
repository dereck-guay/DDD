using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DisableButton : MonoBehaviour
{
    public Button spellButton;
    public Sprite buttonSprite;
    public Sprite disabledSprite;
    public float counter;

    void Start()
    {
        spellButton = GetComponent<Button>();
        spellButton.onClick.AddListener(disableButton);
    }
    public void disableButton()
    {
        spellButton.interactable = false;
        spellButton.image.overrideSprite = disabledSprite;
    }
    
}
