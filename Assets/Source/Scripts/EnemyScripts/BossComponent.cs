using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WanderingAIComponent))]
public class BossComponent : EnemyMonoBehaviour
{
    public RingAttackComponent[] rings;

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

        entityStats.HP.OnTakeDamage += (damage, player) => AddScore(player, (int)damage);

        StartCoroutine("UpdateStats");
    }

    void Update()
    {
        TryCastAttack(ref cooldown);
    }

    IEnumerator UpdateStats()
    {
        float previousModifier = 0;
        float currentModifier;

        while (true)
        {
            currentModifier = 1 - entityStats.HP.Current / entityStats.HP.Base; //0 -> 1

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
        //Applied modifer transitions from 1 to "maxMultiplier".
        entityStats.AtkDamage.ApplyModifier(1 - modifier + damageMultiplier * modifier);
        entityStats.AtkSpeed.ApplyModifier(1 - modifier + atkSpeedMultiplier * modifier);
        entityStats.Speed.ApplyModifier(1 - modifier + speedMultiplier * modifier);
    }

    void EndModifierToStats(float modifier)
    {
        entityStats.AtkDamage.EndModifier(1 - modifier + damageMultiplier * modifier);
        entityStats.AtkSpeed.EndModifier(1 - modifier + atkSpeedMultiplier * modifier);
        entityStats.Speed.EndModifier(1 - modifier + speedMultiplier * modifier);
    }

    void AddScore(PlayerMonoBehaviour player, int value) => player.AddScore(value);

    protected override void Attack(Transform target)
    {
        foreach (var r in rings)
            r.Attack();
    }
}
