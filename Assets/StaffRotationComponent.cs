using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffRotationComponent : MonoBehaviour
{
    float direction = 1;
    void Update()
    {
        if (transform.rotation.eulerAngles.x >= 45)
            direction = -1;
        if (transform.rotation.eulerAngles.x < 6.8)
            Destroy(this);
        Rotate(direction);
    }
    void Rotate(float direction)
    {
        transform.Rotate(new Vector3(1, 0, 0), direction * 90f * Time.deltaTime);
    }
}
