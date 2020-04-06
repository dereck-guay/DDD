using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class AreaCostsData
{
    public int areaIndex;
    public float areaCost = 1;
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class TargetingAIComponent : MonoBehaviour
{
    public int targetLayer;
    public SphereCollider detectionRange;
    public AreaCostsData[] areaCosts;

    [HideInInspector]
    public List<Transform> targetsInRange;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool isStunned;
    [HideInInspector]
    public bool isStopped;

    public bool HasTarget { get => targetsInRange.Count != 0; }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (var a in areaCosts)
            agent.SetAreaCost(a.areaIndex, a.areaCost);
    }

    void Start()
    {
        targetsInRange = new List<Transform>(4);
        detectionRange.isTrigger = true;
    }

    void Update()
    {
        if (HasTarget && !isStunned && !isStopped)
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
