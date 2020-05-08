using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffRotationComponent : MonoBehaviour
{
    [SerializeField] protected Vector3 m_from = new Vector3(0.0F, 0F, 0.0F);
    [SerializeField] protected Vector3 m_to = new Vector3(25.0F, 0F, 0.0F);
    [SerializeField] protected float m_frequency = 1.5F;
    bool reachedMax = false;
    const float MaxEulerAngle = 10;
    float elapsedTime;
    protected virtual void Update()
    {
        if (transform.localRotation.eulerAngles.x > MaxEulerAngle)
            reachedMax = true;
        Quaternion from = Quaternion.Euler(m_from);
        Quaternion to = Quaternion.Euler(m_to);

        float lerp = 0.5f * (1.0F + Mathf.Sin(Mathf.PI * elapsedTime * m_frequency));
        transform.localRotation = Quaternion.Lerp(from, to, lerp);
        if (transform.localRotation.eulerAngles.x < 0.01 && reachedMax)
            Destroy(this);
        elapsedTime += Time.deltaTime;
    }
}
//Fortement inspiré de ce code ci
//https://forum.unity.com/threads/rotate-object-back-and-forth-x-degrees.375899/