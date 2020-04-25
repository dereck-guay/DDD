using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StraightProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float speed = 50f;

    public float damage;

    private void Start() =>
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
}
