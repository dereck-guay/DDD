using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackCollision : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithAppropriateLayer(collision.collider.gameObject.layer, collisionLayers))
        {
            Destroy(gameObject);

            if (CollidesWithAppropriateLayer(collision.collider.gameObject.layer, damageLayers))
                collision.collider.GetComponentInParent<Stats>().HP.TakeDamage(damage);
        }
    }
}
