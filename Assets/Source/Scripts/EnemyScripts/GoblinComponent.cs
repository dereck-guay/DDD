using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(TargetingAIComponent))]
public class GoblinComponent : EntityMonoBehaviour
{
    TargetingAIComponent targetAI;
    public StatsInit statsInit;

    public float knockback;
    float cooldown = 0;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit.attackDamage, statsInit.attackSpeed, statsInit.maxHp, statsInit.hpRegen, statsInit.maxMana, statsInit.manaRegen, statsInit.range, statsInit.speed);

        targetAI = GetComponent<TargetingAIComponent>();

        entityStats.HP.OnDeath += delegate { Destroy(gameObject); };
        entityStats.Speed.OnSpeedChanged += (float newSpeed) => targetAI.agent.speed = newSpeed;
        entityStats.Speed.OnSpeedChanged += (float newSpeed) => Debug.Log($"New speed is {newSpeed}");
    }

    void Update()
    {
        cooldown += Time.deltaTime;

        if (targetAI.HasTarget && cooldown > entityStats.AtkSpeed.Current)
            if (Vector3.Distance(targetAI.targetsInRange[0].position, transform.position) < entityStats.Range.Current)
            {
                Attack(targetAI.targetsInRange[0]);
                cooldown = 0;
            }
    }

    void Attack(Transform target)
    {
        Debug.Log(target);
        target.GetComponent<Stats>().HP.TakeDamage(entityStats.AtkDamage.Current);
        target.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * knockback);
    }

}
