using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingAIComponent))]
public class GolemComponent : EnemyMonoBehaviour
{
    public MeshRenderer core;
    public Color coreDefaultColor, coreChargedColor;
    public float chargeTime;
    TargetingAIComponent targetAI;

    float cooldown = 0;
    float currentChargeTime = 0;
    bool charging = false;

    void Start()
    {
        targetAI = GetComponent<TargetingAIComponent>();
        core.material.color = coreDefaultColor;

        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        targetAI = GetComponent<TargetingAIComponent>();

        entityStats.HP.OnDeath += () => Destroy(gameObject);
        entityStats.Speed.OnSpeedChanged += newSpeed => targetAI.agent.speed = newSpeed;
        OnStunChanged += isStunned => targetAI.isStunned = isStunned;
    }
    
    void Update()
    {
        if (charging)
            Charge();
        else
            TryCastAttack(ref cooldown);
    }

    protected override void Attack(Transform target)
    {
        charging = true;
        targetAI.isStopped = true;
    }

    void Charge()
    {
        currentChargeTime += Time.deltaTime;

        if (currentChargeTime >= chargeTime)
        {
            core.material.color = coreDefaultColor;
            charging = false;
            targetAI.isStopped = false;
            currentChargeTime = 0;
            //+LAUNCH!
        }
        else
        {
            core.material.color = Color.Lerp(coreDefaultColor, coreChargedColor, currentChargeTime / chargeTime);
        }
    }
}
