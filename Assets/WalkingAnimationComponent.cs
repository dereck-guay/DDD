using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Limbs
{
    public Transform rightArm, leftArm, rightLeg, leftLeg;

    public Transform[] GetLimbs() => new Transform[] { rightArm, leftArm, leftLeg, rightLeg };
}

public class WalkingAnimationComponent : MonoBehaviour
{
    const float BasePeriod = 2 * Mathf.PI;
    public float amplitude = 45;
    public float period = 1;
    public Limbs limbsI;

    Transform[] limbs;
    bool isWalking = false;
    bool isStatic = true;
    float time = 0;

    public void Walk()
    {
        isWalking = true;
        isStatic = false;
    }

    public void Stop() => isWalking = false;

    void Start() => limbs = limbsI.GetLimbs();

    void Update()
    {
        if (isWalking)
        {
            time = (time + Time.deltaTime) % period;
            UpdateAnimation(time);
        }
        else
        {
            if (!isStatic)
            {
                Reset();
                isStatic = true;
            }
        }
    }

    void Reset()
    {
        foreach (var l in limbs)
            l.localEulerAngles = Vector3.zero;
    }

    void UpdateAnimation(float time)
    {
        for (byte b = 0; b < limbs.Length; ++b)
            limbs[b].localEulerAngles = new Vector3(amplitude * Mathf.Sin((b % 2 == 0 ? 1 : -1) * time * BasePeriod / period), 0, 0);
    }
}
