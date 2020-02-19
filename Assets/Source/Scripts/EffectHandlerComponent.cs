using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandlerComponent : MonoBehaviour
{
    public PlayerMonoBehaviour player;

    public void ApplyEffect(int statIndex, float value)
    {
        player.stats.ModifiableStats[statIndex].ApplyModifier(value);
    }

    public void EndEffect(int statIndex, float value)
    {
        player.stats.ModifiableStats[statIndex].EndModifier(value);
    }
}
