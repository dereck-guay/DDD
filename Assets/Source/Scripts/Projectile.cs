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

public class FollowProjectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public GameObject target;
    public float speed = 5f;
    
    void Update() 
    {
        RecalculateDirection();    
        transform.Translate(direction * speed * Time.deltaTime);
    }

   private void RecalculateDirection() =>
       direction = Vector3.Normalize(target.transform.position - transform.position);
}
