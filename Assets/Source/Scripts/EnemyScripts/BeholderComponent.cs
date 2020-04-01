using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActiveEffect
{
    public EffectHandlerComponent Target { get; private set; }
    public ModifiableStats Stat { get; private set; }
    public float Modifier { get; private set; }
    public float MaxDuration { get; private set; }
    public float CurrentDuration { get; private set; }

    public ActiveEffect(EffectHandlerComponent targetI, ModifiableStats statI, float modifierI, float maxDurationI)
    {
        Target = targetI;
        Stat = statI;
        Modifier = modifierI;
        MaxDuration = maxDurationI;
        CurrentDuration = 0;
    }

    public bool UpdateActiveEffect(float elapsedTime)
    {
        CurrentDuration += elapsedTime;
        return CurrentDuration > MaxDuration;
    }
}

[RequireComponent(typeof(TargetingAIComponent))]
public class BeholderComponent : EnemyMonoBehaviour
{
    TargetingAIComponent targetAI;
    public GameObject projectile;
    public Transform exit;
    
    float cooldown = 0;

    List<ActiveEffect> activeEffects;

    void Start()
    {
        entityStats = GetComponent<Stats>();
        entityStats.ApplyStats(statsInit);

        activeEffects = new List<ActiveEffect>(4);

        targetAI = GetComponent<TargetingAIComponent>();
        Debug.Assert(projectile.GetComponent<BeholderCollisionComponent>());

        entityStats.HP.OnDeath += () => Destroy(gameObject);
        entityStats.HP.OnDeath += () => EndAllActiveEffects();
        entityStats.Speed.OnSpeedChanged += newSpeed => targetAI.agent.speed = newSpeed;
    }

    void Update()
    {
        TryCastAttack(targetAI, ref cooldown);
        UpdateActiveEffects();
    } 

    protected override void Attack(Transform target) => 
        Instantiate(projectile, exit.position, exit.rotation).GetComponent<BeholderCollisionComponent>().SetCaster(this);

    void UpdateActiveEffects()
    {
        float elapsedTime = Time.deltaTime;
        ActiveEffect currentEffect;

        for (byte b = 0; b < activeEffects.Count; b++)
        {
            currentEffect = activeEffects[b];
            if (currentEffect.UpdateActiveEffect(elapsedTime)) //if effect is over
            {
                EndActiveEffect(currentEffect);
                activeEffects.RemoveAt(b);
            }
        }            
    } 

    void EndAllActiveEffects()
    {
        foreach (var e in activeEffects)
            EndActiveEffect(e);
    }

    void EndActiveEffect(ActiveEffect effect) => effect.Target.EndEffect((int)effect.Stat, effect.Modifier);

    public void AddActiveEffect(ActiveEffect effect) => activeEffects.Add(effect);
}
