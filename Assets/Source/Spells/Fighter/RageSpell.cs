using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RageSpell : SpellMonoBehavior
{
    public float atkSpeedValue = 1;
    public void Cast() =>
        GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.AtkSpeed, atkSpeedValue);

    private void Update()
    {
        if (currentLifeTime >= cooldown)
        {
            GetComponent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.AtkSpeed, atkSpeedValue);
            Destroy(this);
        }

        currentLifeTime += Time.deltaTime;
    }
}
