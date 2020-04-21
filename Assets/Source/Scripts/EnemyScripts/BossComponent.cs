using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WanderingAIComponent))]
public class BossComponent : EnemyMonoBehaviour
{
    [Header("MaxStatsMultipliers")]
    public float damageMultiplier;
    public float atkSpeedMultiplier;
    public float speedMultiplier;

    WanderingAIComponent wanderingAI;

    float cooldown;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        wanderingAI = GetComponent<WanderingAIComponent>();

        entityStats.Speed.OnSpeedChanged += newSpeed => wanderingAI.agent.speed = newSpeed;
        OnStunChanged += IsStunned => wanderingAI.isStunned = IsStunned;

        StartCoroutine("UpdateStats");
    }

    IEnumerator UpdateStats()
    {
        float previousModifier = 0;
        float currentModifier;

        while (true)
        {
            currentModifier = 1 - entityStats.HP.Current / entityStats.HP.Base;

            if (currentModifier != previousModifier)
            {
                EndModifierToStats(previousModifier);
                ApplyModifierToStats(currentModifier);

                previousModifier = currentModifier;
            }
            yield return new WaitForSeconds(5);
        }
    }

    void ApplyModifierToStats(float modifier)
    {
        entityStats.AtkDamage.ApplyModifier(1 + damageMultiplier * modifier);
        entityStats.AtkSpeed.ApplyModifier(1 + atkSpeedMultiplier * modifier);
        entityStats.Speed.ApplyModifier(1 + speedMultiplier * modifier);
    }

    void EndModifierToStats(float modifier)
    {
        entityStats.AtkDamage.EndModifier(1 + damageMultiplier * modifier);
        entityStats.AtkSpeed.EndModifier(1 + atkSpeedMultiplier * modifier);
        entityStats.Speed.EndModifier(1 + speedMultiplier * modifier);
    }

    protected override void Attack(Transform target)
    {
        //1) Update rings rotation
        //2) Update exit points arrangement
        //3) LAUNCH!
    }
}
