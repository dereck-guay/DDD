using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderCollisionComponent : CollisionMonoBehaviour
{
    Action<EffectHandlerComponent>[] effects;

    public int[] damageLayers;
    public float damage;    
    public float effectBaseModifier;    
    public float effectBaseDuration;
    public float maxDuration;

    static BeholderComponent caster;   //Manages effects durations

    float currentTime;
    Vector3 baseScale;

    private void Start()
    {
        baseScale = gameObject.transform.localScale;
        effects = new Action<EffectHandlerComponent>[]
        {
            (eHandler) => { eHandler.GetComponent<Stats>().HP.TakeDamage(damage); },
            (eHandler) => { caster.AddActiveEffect(new ActiveEffect(eHandler, ModifiableStats.Speed, effectBaseModifier, effectBaseDuration)); },
            (eHandler) => { caster.AddActiveEffect(new ActiveEffect(eHandler, ModifiableStats.Speed, effectBaseModifier / 2, effectBaseDuration / 2)); },
            (eHandler) => { caster.AddActiveEffect(new ActiveEffect(eHandler, ModifiableStats.HPRegen, effectBaseModifier / 4, effectBaseDuration)); }
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if (CollidesWithAppropriateLayer(target.layer, collisionLayers))
        {
            Destroy(gameObject);
            if (CollidesWithAppropriateLayer(target.layer, damageLayers))
                ApplyRandomEffect(target.GetComponent<EffectHandlerComponent>());
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        gameObject.transform.localScale = baseScale * Mathf.Sqrt(Mathf.Abs(maxDuration - currentTime) / maxDuration);

        if (currentTime >= maxDuration)
            Destroy(gameObject);
    }

    public void SetCaster(BeholderComponent casterI) => caster = casterI;

    void ApplyRandomEffect(EffectHandlerComponent target)
    {
        if (caster)
            effects[UnityEngine.Random.Range(0, effects.Length)].Invoke(target);
    }
}
