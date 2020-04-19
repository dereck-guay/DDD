using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

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
    
    WalkingAnimationComponent walkingAnimation;
    WanderingAIComponent wanderingAI;

    public bool HasTarget { get => targetsInRange.Count != 0; }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        foreach (var a in areaCosts)
            agent.SetAreaCost(a.areaIndex, a.areaCost);
    }

    void Start()
    {
        enabled = false;

        targetsInRange = new List<Transform>(4);
        detectionRange.isTrigger = true;
        TryGetComponent(out walkingAnimation);
        TryGetComponent(out wanderingAI);
    }

    void Update()
    {
        if (!isStunned)
        {
            if (HasTarget)
                agent.SetDestination(targetsInRange[0].position);
            else
            {
                enabled = false;
                wanderingAI?.Go();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetLayer == other.gameObject.layer)
        {
            targetsInRange.Add(other.transform);
            enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetsInRange.Contains(other.transform))
            targetsInRange.Remove(other.transform);
    }

    private void OnEnable()
    {
        agent.enabled = true;
        walkingAnimation?.Walk();
        wanderingAI?.Stop();
    }

    private void OnDisable()
    {
        agent.enabled = false;
        walkingAnimation?.Stop();
    }
}
