using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TakeABreatherSpell : MonoBehaviour
{
    public float cooldown = 1;
    public float regenValue = 1;

    private float currentLifeTime;
    public void Cast() =>
        GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.HPRegen, regenValue);

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
