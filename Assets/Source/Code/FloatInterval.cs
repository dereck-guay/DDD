using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatInterval
{
    public float min, max;

    float? current = null;
    public float Current
    {
        get
        {
            if (current.HasValue)
                return current.Value;
            else
                return NewRandomValue();
        }
        private set { }
    }
    public float NewRandomValue()
    {
        current = Random.Range(min, max);
        return current.Value;
    }
}
