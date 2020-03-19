using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    public GameObject projectile;
    void Update()
    {
        if(projectile)
            transform.LookAt(projectile.transform);
    }
}
