using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
   public GameObject[] exits;
   public GameObject projectile;

   public float delay;
   float elapsedTime = 0;

   void Update()
   {
      elapsedTime += Time.deltaTime;

      if (elapsedTime >= delay)
      {
         elapsedTime -= delay;
         foreach (var exit in exits)
            Instantiate(projectile, exit.transform.position, exit.transform.rotation);
      }
   }
}
