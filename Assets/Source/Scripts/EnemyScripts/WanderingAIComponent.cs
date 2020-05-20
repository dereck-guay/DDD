using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class WanderingAIComponent : MonoBehaviour
{
    const float DistanceBufferMultiplier = 3;

    public bool staysNearSpawn;
    public float radius;

    public FloatInterval delay;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool isStunned;

    float cooldown = 0;
    Vector3 spawnPosition;
    WalkingAnimationComponent walkingAnimation;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        TryGetComponent(out walkingAnimation);
        spawnPosition = transform.position;
    }
    
    void Update()
    {
        if (agent.enabled && agent.remainingDistance == 0)
        {
            agent.enabled = false;
            walkingAnimation?.Stop();
        }

        if (!isStunned)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= delay.Current)
            {
                cooldown -= delay.Current;
                delay.NewRandomValue();

                agent.enabled = true;
                StartCoroutine("SetDestination");
                walkingAnimation?.Walk();
            }
        }
        else if (agent.enabled)
            agent.enabled = false;
    }

    IEnumerator SetDestination()
    {
        Vector3 destination;
        float angle, distance;
        
        do
        {
            angle = Random.Range(0, 2 * Mathf.PI);
            distance = Random.Range(staysNearSpawn ? 0 : radius / 2, radius);
            destination = (staysNearSpawn ? spawnPosition : transform.position) + new Vector3(Mathf.Cos(angle), transform.position.y, Mathf.Sin(angle)) * distance;
            agent.SetDestination(destination);

            yield return new WaitWhile(() => agent.pathPending);

            if (IsInvalidPath(agent.path))
            {
                agent.enabled = false;
                agent.enabled = true;
            }
        }
        while (agent.remainingDistance > DistanceBufferMultiplier * distance || agent.remainingDistance == 0);
    }

    //https://forum.unity.com/threads/solved-test-if-the-navmesh-agent-has-arrived-at-the-targeted-location.327753/
    bool IsInvalidPath(NavMeshPath path) => path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial;

    public void Go()
    {
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }
}
