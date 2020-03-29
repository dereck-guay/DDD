using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TargetingAIComponent : MonoBehaviour
{
   public int targetLayer;
   [HideInInspector]
   public List<Transform> targetsInRange;
   [HideInInspector]
   public NavMeshAgent agent;
   public SphereCollider detectionRange;

   public bool HasTarget { get => targetsInRange.Count != 0; }

   void Start()
   {
      targetsInRange = new List<Transform>(4);
      agent = GetComponent<NavMeshAgent>();
      detectionRange.isTrigger = true;
   }

   void Update()
   {
      if (HasTarget)
         agent.SetDestination(targetsInRange[0].position);
      else
         agent.SetDestination(transform.position);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.layer == targetLayer)
         targetsInRange.Add(other.transform);
   }

   private void OnTriggerExit(Collider other)
   {
      if (targetsInRange.Contains(other.transform))
         targetsInRange.Remove(other.transform);
   }
}
