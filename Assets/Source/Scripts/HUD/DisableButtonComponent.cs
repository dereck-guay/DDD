using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DisableButtonComponent : MonoBehaviour
{
    public Button spellButton;
    public Sprite disabledSprite;
    public Sprite enabledSprite;

    public void DisableButton()
    {
        spellButton.interactable = false;
        spellButton.image.sprite = disabledSprite;
    }

    public void EnableButton()
    {

    }
}
