using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerCollision : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;
    public float maxLifeTime;
    public PlayerMonoBehaviour caster;
    private float currentLifeTime;
    private void OnTriggerEnter(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers))
        {
            Destroy(gameObject);

            if (CollidesWithAppropriateLayer(other.gameObject.layer, damageLayers))
                other.GetComponentInParent<Stats>().HP.TakeDamage(damage, caster);
        }
    }
    private void Update()
    {
        if (currentLifeTime >= maxLifeTime)
            Destroy(gameObject);
        currentLifeTime += Time.deltaTime;
    }
}
