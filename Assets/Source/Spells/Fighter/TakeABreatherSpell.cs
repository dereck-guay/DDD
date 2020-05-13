using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TakeABreatherSpell : SpellMonoBehavior
{
    public float regenValue = 1;
    readonly string audioName = "Fighter Take A Breather";
    public void Cast()
    {
        Play(audioName);
        GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.HPRegen, regenValue);
    }
    private void Update()
    {
        if (currentLifeTime >= cooldown)
        {
            GetComponent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.HPRegen, regenValue);
            Destroy(this);
        }
        currentLifeTime += Time.deltaTime;
    }
}
