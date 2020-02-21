using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBodyComponent : MonoBehaviour
{
    public Collider triggerCollider;
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }
}
