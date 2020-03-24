using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverTextComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject childImage;
    void Start() => childImage.SetActive(false);
    private void Update()
    {
        if (PauseMenuComponent.GameIsPaused == true)
            childImage.SetActive(false);
    }

    //Detects if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData) =>
        childImage.SetActive(true);

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData) =>
        childImage.SetActive(false);
}
