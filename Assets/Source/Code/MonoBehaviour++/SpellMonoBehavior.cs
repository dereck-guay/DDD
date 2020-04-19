using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellMonoBehavior : MonoBehaviour
{
    public float cooldown = 1;
    protected float currentLifeTime;
}
