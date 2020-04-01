using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingAIComponent))]
public class GoblinComponent : EntityMonoBehaviour
{
    TargetingAIComponent targetAI;

    public float knockback;
    float cooldown = 0;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        targetAI = GetComponent<TargetingAIComponent>();

        entityStats.HP.OnDeath += delegate { Destroy(gameObject); };
        entityStats.Speed.OnSpeedChanged += (float newSpeed) => targetAI.agent.speed = newSpeed;
        OnStunChanged += isStunned => targetAI.isStunned = isStunned;
    }

    void Update()
    {
        cooldown += Time.deltaTime;

        if (targetAI.HasTarget && cooldown > entityStats.AtkSpeed.Current && !IsStunned)
            if (Vector3.Distance(targetAI.targetsInRange[0].position, transform.position) < entityStats.Range.Current)
            {
                Attack(targetAI.targetsInRange[0]);
                cooldown = 0;
            }
    }

    void Attack(Transform target)
    {
        target.GetComponent<Stats>().HP.TakeDamage(entityStats.AtkDamage.Current);
        target.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * knockback);
    }

}
