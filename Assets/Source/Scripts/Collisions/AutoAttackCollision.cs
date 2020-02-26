using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackCollision : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (CollidesWithAppropriateLayer(other.gameObject.layer, collisionLayers))
        {
            Destroy(gameObject);
            
            if (CollidesWithAppropriateLayer(other.gameObject.layer, damageLayers))
                other.GetComponentInParent<Stats>().HP.TakeDamage(damage);
        }
    }
}
