using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public GameObject target;
    public float speed;

    void Update()
    {
        RecalculateDirection();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void RecalculateDirection() =>
        direction = Vector3.Normalize(target.transform.position - transform.position);
}