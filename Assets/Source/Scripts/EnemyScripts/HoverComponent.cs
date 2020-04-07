using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverComponent : MonoBehaviour
{
    const float BasePeriod = 2 * Mathf.PI;
    public float amplitude = 1;
    public float period = 1;
    float baseHeight;
    float time = 0;

    void Start() => baseHeight = transform.localPosition.y;

    void Update()
    {
        time = (time + Time.deltaTime) % period;
        transform.localPosition = new Vector3(transform.localPosition.x, baseHeight + amplitude * Mathf.Sin(time * BasePeriod / period), transform.localPosition.z);
    }
}
