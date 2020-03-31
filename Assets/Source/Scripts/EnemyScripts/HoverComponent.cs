using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverComponent : MonoBehaviour
{
    const float BasePeriod = 2 * Mathf.PI;
    public float amplitude = 1;
    public float period = 1;
    float baseHeight;
    float elapsedTime = 0;

    void Start() => baseHeight = transform.localPosition.y;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, baseHeight + amplitude * Mathf.Sin(elapsedTime * BasePeriod / period), transform.localPosition.z);
    }
}
