using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(TargetingAIComponent))]
public class GoblinComponent : MonoBehaviour
{
    Stats stats;
    TargetingAIComponent targetAI;
    public StatsInit statsInit;

    public float knockback;
    float cooldown = 0;

    void Start()
    {
        stats = GetComponent<Stats>();
        stats.ApplyStats(statsInit.attackDamage, statsInit.attackSpeed, statsInit.maxHp, statsInit.hpRegen, statsInit.maxMana, statsInit.manaRegen, statsInit.range, statsInit.speed);

        targetAI = GetComponent<TargetingAIComponent>();

        stats.HP.OnDeath += delegate { Destroy(gameObject); };
        stats.Speed.OnSpeedChanged += (float newSpeed) => targetAI.agent.speed = newSpeed;
        stats.Speed.OnSpeedChanged += (float newSpeed) => Debug.Log($"New speed is {newSpeed}");
    }

    void Update()
    {
        cooldown += Time.deltaTime;

        if (targetAI.HasTarget && cooldown > stats.AtkSpeed.Current)
            if (Vector3.Distance(targetAI.targetsInRange[0].position, transform.position) < stats.Range.Current)
            {
                Attack(targetAI.targetsInRange[0]);
                cooldown = 0;
            }
    }

    void Attack(Transform target)
    {
        Debug.Log(target);
        target.GetComponent<Stats>().HP.TakeDamage(stats.AtkDamage.Current);
        target.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * knockback);
    }

}
