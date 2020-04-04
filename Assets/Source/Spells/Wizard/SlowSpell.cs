using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpell : MonoBehaviour
{
    public GameObject target;
    public float slowValue;
    public float effectDuration;

    public float cooldown = 10f;
    public float currentLifeTime;
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
