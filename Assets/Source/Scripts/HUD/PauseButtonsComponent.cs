using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButtonsComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GetComponent<Button>().transform.localScale += new Vector3(.1f, .1f, .1f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GetComponent<Button>().transform.localScale += new Vector3(-.1f, -.1f, -.1f);
    }
}
