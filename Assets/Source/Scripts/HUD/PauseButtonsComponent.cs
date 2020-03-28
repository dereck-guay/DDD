using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButtonsComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject btn;
<<<<<<< HEAD
    
=======

>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        btn.GetComponent<Button>().transform.localScale += new Vector3(.1f, .1f, .1f);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
<<<<<<< HEAD
=======
        //btn.transform.localScale += new Vector3(-.1f, -.1f, -.1f);
>>>>>>> ca977158dae0d27f8cbb56efe67450a78aebb4fd
        btn.GetComponent<Button>().transform.localScale += new Vector3(-.1f, -.1f, -.1f);
    }
}
