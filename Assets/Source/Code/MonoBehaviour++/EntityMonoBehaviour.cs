using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EffectHandlerComponent))]
public abstract class EntityMonoBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Stats entityStats;
    public StatsInit statsInit;
    private bool isStunned = false;
    public bool IsStunned
    {
        get { return isStunned; }
        set
        {
            isStunned = value;
            OnStunChanged?.Invoke(isStunned);
        }
    }
    protected Action<bool> OnStunChanged;
}
