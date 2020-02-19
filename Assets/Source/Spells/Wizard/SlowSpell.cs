using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpell : MonoBehaviour
{
    public GameObject target;
    public float slowValue;
    public float[] effectDurations = { 2f, 4f, 6f };

    private float[] cooldowns = { 10f, 9f, 8f };
    public float currentLifeTime;
    private int playerLevel;
    private bool isActive;

    public void Cast()
    {
        target.GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.Speed, slowValue);
        playerLevel = GetComponent<XP>().Level;
        isActive = true;
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel])
            Destroy(this);
        else if(currentLifeTime >= effectDurations[playerLevel] && isActive)
        {
            target.GetComponent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.Speed, slowValue);
            isActive = false;
        }

        currentLifeTime += Time.deltaTime;
    }
}
