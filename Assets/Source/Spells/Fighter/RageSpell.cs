using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class RageSpell : MonoBehaviour
{
    public float cooldown = 1;
    public float atkSpeedValue = 1;

    private float currentLifeTime;
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
