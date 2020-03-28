using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TakeABreatherSpell : MonoBehaviour
{
    private float[] cooldowns = { 10f, 9f, 8f };
    public float currentLifeTime;
    private int playerLevel = 1;
    private float[] regenValue = { 2f, 2.5f, 3f };
    public void Cast(int level)
    {
        playerLevel = level;
        GetComponent<EffectHandlerComponent>().ApplyEffect((int)ModifiableStats.HPRegen, regenValue[playerLevel - 1]);
    }

    private void Update()
    {
        if (currentLifeTime >= cooldowns[playerLevel - 1])
        {
            GetComponent<EffectHandlerComponent>().EndEffect((int)ModifiableStats.HPRegen, regenValue[playerLevel - 1]);
            Destroy(this);
        }

        currentLifeTime += Time.deltaTime;
    }
}
