using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderCollisionComponent : CollisionMonoBehaviour
{
    Action<Stats>[] Effects = new Action<Stats>[]
    {
        (stats) => { stats.HP.TakeDamage(damage); },
        (stats) => {}  //Slow is in BeholderComponent
    };

    public int[] damageLayers;
    public static float damage;
    public static float slowFactor;
    public float maxDuration;
    [HideInInspector]
    public BeholderComponent caster;   //Manages effects durations
    
    float currentTime;
    Vector3 baseScale;

    private void Start()
    {
        baseScale = gameObject.transform.localScale;        
    }     

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if (CollidesWithAppropriateLayer(target.layer, collisionLayers))
        {
            Destroy(gameObject);
            if (CollidesWithAppropriateLayer(target.layer, damageLayers))
                target.GetComponent<Stats>().HP.TakeDamage(damage);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        gameObject.transform.localScale = baseScale * Mathf.Sqrt(Mathf.Abs(maxDuration - currentTime) / maxDuration);

        if (currentTime >= maxDuration)
            Destroy(gameObject);
    }

    void GiveRandomEffect(Stats targetStats)
    {

    }
}
