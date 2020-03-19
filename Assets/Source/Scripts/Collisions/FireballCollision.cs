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
      if (CollidesWithAppropriateLayer(collision.gameObject.layer, collisionLayers))
         Explode(collision.gameObject);
   }

   private void Explode(GameObject target)
   {
      Destroy(gameObject);
      Instantiate(collideEffect as ParticleSystem, transform.position, Quaternion.identity);
      if (CollidesWithAppropriateLayer(target.layer, damageLayers))
         target.GetComponent<Stats>().HP.TakeDamage(damage);
   }
}
