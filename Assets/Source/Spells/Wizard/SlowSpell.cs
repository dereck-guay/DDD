using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpell : SpellMonoBehavior
{
    public GameObject target;
    public float slowValue;
    public float effectDuration;
    private bool isActive;

    public void Cast()
    {
        target.GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.Speed, slowValue);
        isActive = true;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldown)
            Destroy(this);
        else if(currentLifeTime >= effectDuration && isActive)
        {
            target.GetComponent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.Speed, slowValue);
            isActive = false;
        }

        currentLifeTime += Time.deltaTime;
    }
}
