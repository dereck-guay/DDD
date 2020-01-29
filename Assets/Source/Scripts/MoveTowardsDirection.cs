using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsDirection : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float movementSpeed = 50f;
    public float lifeTime = 5f;

    private float currentLifeTime = 0f;
    void Update()
    {
        if (currentLifeTime >= lifeTime)
            Destroy(gameObject);

        transform.Translate(direction * movementSpeed * Time.deltaTime);

        currentLifeTime += Time.deltaTime;
    }
}
