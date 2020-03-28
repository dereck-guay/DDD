using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IcePatchComponent : MonoBehaviour
{
    [HideInInspector]
    public float lifeSpan;
    public float spinIntensity;

    float currentLifeTime = 0;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, Random.Range(-spinIntensity, spinIntensity), 0);
    }
    
    void Update()
    {
        currentLifeTime += Time.deltaTime;

        if (currentLifeTime >= lifeSpan)
            Destroy(gameObject);
    }
}
