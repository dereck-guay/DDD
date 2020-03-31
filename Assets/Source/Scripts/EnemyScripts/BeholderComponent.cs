using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingAIComponent))]
public class BeholderComponent : EntityMonoBehaviour
{
    TargetingAIComponent targetAI;
    public GameObject projectile;
    public Transform exit;
    
    float cooldown = 0;
    bool isSlowingTarget;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);
        isStunned = false;

        targetAI = GetComponent<TargetingAIComponent>();
        isSlowingTarget = false;

        entityStats.HP.OnDeath += () => Destroy(gameObject);
        entityStats.HP.OnDeath += () => { };
        entityStats.Speed.OnSpeedChanged += (float newSpeed) => targetAI.agent.speed = newSpeed;
    }

    void Update()
    {
        cooldown += Time.deltaTime;

        if (targetAI.HasTarget && cooldown > entityStats.AtkSpeed.Current)
            if (Vector3.Distance(targetAI.targetsInRange[0].position, transform.position) < entityStats.Range.Current)
            {
                Attack();
                cooldown = 0;
            }
    }

    void Attack() => Instantiate(projectile, exit.position, exit.rotation);
}
