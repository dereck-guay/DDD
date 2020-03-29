using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButtonsComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button btn;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        btn.transform.localScale += new Vector3(.1f, .1f, .1f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        btn.transform.localScale += new Vector3(-.1f, -.1f, -.1f);
    }
}
