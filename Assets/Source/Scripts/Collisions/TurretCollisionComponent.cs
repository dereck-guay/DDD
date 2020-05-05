using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TurretCollisionComponent : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;

        if (CollidesWithAppropriateLayer(target.layer, collisionLayers))
        {
            Destroy(gameObject);
            if (CollidesWithAppropriateLayer(target.layer, damageLayers))
                target.GetComponent<Stats>().HP.TakeDamage(damage, null);
        }
    }
}
