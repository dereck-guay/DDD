using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandlerComponent : MonoBehaviour
{
    public EntityMonoBehaviour entity;

    public void ApplyEffect(int statIndex, float value)
    {
        entity.entityStats.ModifiableStats[statIndex].ApplyModifier(value);
    }

    public void EndEffect(int statIndex, float value)
    {
        entity.entityStats.ModifiableStats[statIndex].EndModifier(value);
    }
}
