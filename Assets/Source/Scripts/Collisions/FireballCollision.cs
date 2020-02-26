using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollision : CollisionMonoBehaviour
{
    public int[] damageLayers;
    public float damage;
    public ParticleSystem collideEffect;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (CollidesWithAppropriateLayer(collision.gameObject.layer, collisionLayers))
            Explode(collision.gameObject);
    }
    private void Explode(GameObject target)
    {
        
        Destroy(gameObject);
        Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
        if (CollidesWithAppropriateLayer(target.transform.parent.gameObject.layer, damageLayers))
            target.GetComponentInParent<Stats>().HP.TakeDamage(damage);
    }
}
