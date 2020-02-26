using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float speed = 5f;

    void Update() =>
        transform.Translate(direction * speed * Time.deltaTime);
}
