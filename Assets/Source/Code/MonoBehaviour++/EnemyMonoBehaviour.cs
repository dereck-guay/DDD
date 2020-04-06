using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMonoBehaviour : EntityMonoBehaviour
{
    protected virtual void TryCastAttack(TargetingAIComponent targetAI, ref float cooldown)
    {
        cooldown += Time.deltaTime;

        if (targetAI.HasTarget && cooldown > entityStats.AtkSpeed.Current)
            if (Vector3.Distance(targetAI.targetsInRange[0].position, transform.position) < entityStats.Range.Current)
            {
                Attack(targetAI.targetsInRange[0]);
                cooldown = 0;
            }
    }
    protected virtual void TryCastAttack(ref float cooldown)
    {
        cooldown += Time.deltaTime;

        if (cooldown > entityStats.AtkSpeed.Current)
        {
            Attack(null);
            cooldown = 0;
        }
    }
    protected abstract void Attack(Transform target);
}
