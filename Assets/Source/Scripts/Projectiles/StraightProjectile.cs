using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StraightProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float speed = 50f;

    private void Start() =>
        GetComponent<Rigidbody>().AddForce(direction * speed);
}
