using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(EffectHandlerComponent))]
public abstract class EntityMonoBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Stats entityStats;
    public StatsInit statsInit;
    public bool isStunned;
}
