﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FollowProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public GameObject target;
    public float speed;
    private Rigidbody rb;

    private void Start() =>
        rb = GetComponent<Rigidbody>();

    void Update()
    {
        if (!target)
            Destroy(gameObject);

        RecalculateDirection();
        rb.velocity = Vector3.zero;
        rb.AddForce(direction * speed);
        rb.gameObject.transform.LookAt(target.transform);
    }

    private void RecalculateDirection() =>
        direction = Vector3.Normalize(target.transform.position - transform.position);
}