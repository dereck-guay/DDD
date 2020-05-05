using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HazardComponent : CollisionMonoBehaviour
{
   public int[] damageLayers;
   public float damagePerSecond = 10;

   void Start() => GetComponent<BoxCollider>().isTrigger = true;

   private void OnTriggerStay(Collider other)
   {
      if (CollidesWithAppropriateLayer(other.gameObject.layer, damageLayers))
         other.gameObject.GetComponentInParent<Stats>().HP.TakeDamage(damagePerSecond * Time.deltaTime, null);
   }
}
