using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
   public GameObject[] exits;
   public GameObject projectile;

   public float delay;
   public float power;
   float elapsedTime = 0;

   GameObject instantiatedProjectile;

   // Update is called once per frame
   void Update()
   {
      elapsedTime += Time.deltaTime;

      if (elapsedTime >= delay)
      {
         elapsedTime -= delay;
         foreach (var exit in exits)
         {
            instantiatedProjectile = Instantiate(projectile, exit.transform.position, exit.transform.rotation);
            instantiatedProjectile.transform.localScale = Vector3.one / 4;
            instantiatedProjectile.GetComponent<Rigidbody>().AddForce(instantiatedProjectile.transform.forward * power);
         }
      }
   }
}
