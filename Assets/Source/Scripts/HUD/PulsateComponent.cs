using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsateComponent : MonoBehaviour
{
    void Update()
    {
        //GetComponent<Image>().transform.localScale = new Vector3(Mathf.PingPong(Time.time, .2f), Mathf.PingPong(Time.time, .2f), transform.localScale.z);
    }
}
