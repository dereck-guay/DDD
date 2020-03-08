using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePatchComponent : MonoBehaviour
{
    private float currentLifeTime;
    public float maxLifeTime;
    void Update()
    {
        if (currentLifeTime >= maxLifeTime)
            Destroy(gameObject);
        currentLifeTime += Time.deltaTime;
    }
}
