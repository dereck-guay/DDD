using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetingAIComponent))]
public class GolemComponent : EnemyMonoBehaviour
{
    public MeshRenderer core;
    public Color coreDefaultColor, coreChargedColor;
    public float chargeTime;
    public GameObject laserPrefab;
    public Transform exit;
    TargetingAIComponent targetAI;
    WalkingAnimationComponent walkingAnimation;

    float cooldown = 0;
    float currentChargeTime = 0;
    bool charging = false;

    void Start()
    {
        Debug.Assert(laserPrefab.GetComponent<LaserCollisionComponent>());

        targetAI = GetComponent<TargetingAIComponent>();
        core.material.color = coreDefaultColor;

        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        TryGetComponent(out walkingAnimation);
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

    protected override void TryCastAttack(ref float cooldown)
    {
        if (targetAI.HasTarget)
            base.TryCastAttack(ref cooldown);
    }

    protected override void Attack(Transform target)
    {
        charging = true;
        targetAI.enabled = false;
        walkingAnimation?.Stop();
    }

    void Charge()
    {
        currentChargeTime += Time.deltaTime;

        if (currentChargeTime >= chargeTime)
        {
            core.material.color = coreDefaultColor;
            charging = false;
            targetAI.enabled = true;
            currentChargeTime = 0;
            Instantiate(laserPrefab, exit.position, exit.rotation).GetComponent<LaserCollisionComponent>().damage = entityStats.AtkDamage.Current;
            walkingAnimation?.Walk();
        }
        else
            core.material.color = Color.Lerp(coreDefaultColor, coreChargedColor, currentChargeTime / chargeTime);
    }
}
