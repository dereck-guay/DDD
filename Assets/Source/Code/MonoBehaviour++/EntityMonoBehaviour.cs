using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectHandlerComponent))]
public abstract class EntityMonoBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Stats entityStats;
}
