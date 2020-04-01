using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingAIComponent))]
public class GoblinComponent : EnemyMonoBehaviour
{
    TargetingAIComponent targetAI;

    public float knockback;
    float cooldown = 0;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        targetAI = GetComponent<TargetingAIComponent>();

        entityStats.HP.OnDeath += () => Destroy(gameObject);
        entityStats.Speed.OnSpeedChanged += newSpeed => targetAI.agent.speed = newSpeed;
        OnStunChanged += isStunned => targetAI.isStunned = isStunned;
    }

    void Update() => TryCastAttack(targetAI, ref cooldown);

    protected override void Attack(Transform target)
    {
        target.GetComponent<Stats>().HP.TakeDamage(entityStats.AtkDamage.Current);
        target.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * knockback);
        Debug.Log((target.position - transform.position).normalized);
        Debug.Log(target);
    }

}
