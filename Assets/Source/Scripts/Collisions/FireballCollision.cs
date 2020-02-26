using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : CollisionMonoBehaviour
{
    public float damage;
    public ParticleSystem collideEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (CollidesWithAppropriateLayer(collision.collider.gameObject.layer, collisionLayers))
            Explode(collision.collider.gameObject);
    }
    private void Explode(GameObject target)
    {
        Destroy(gameObject);
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
        if (target.transform.parent.gameObject)
            target.GetComponentInParent<Stats>().HP.TakeDamage(damage);
    }
}
