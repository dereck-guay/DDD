using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RageSpell : SpellMonoBehavior
{
    public float atkSpeedValue = 1;
    readonly string audioName = "Fighter Rage";
    public void Cast()
    {
        Play(audioName);
        GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.AtkSpeed, atkSpeedValue);
    }

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
