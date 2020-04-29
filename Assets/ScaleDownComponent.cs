using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownComponent : MonoBehaviour
{
    public float maxDuration;

    float currentTime;
    Vector3 baseScale;

    void Start() => baseScale = transform.localScale;

    void Update()
    {
        currentTime += Time.deltaTime;
        gameObject.transform.localScale = baseScale * Mathf.Sqrt(Mathf.Abs(maxDuration - currentTime) / maxDuration);

        if (currentTime >= maxDuration)
            Destroy(gameObject);
    }
}
