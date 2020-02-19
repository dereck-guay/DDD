using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject childText;
    public GameObject childImage;
    void Start()
    {
        Text description = GetComponentInChildren<Text>();
        if (description != null)
        {
            childText = description.gameObject;
            childText.SetActive(false);
            childImage.SetActive(false);
        }
    }
    //Detects if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        childText.SetActive(true);
        childImage.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        childText.SetActive(false);
        childImage.SetActive(false);
    }
}
