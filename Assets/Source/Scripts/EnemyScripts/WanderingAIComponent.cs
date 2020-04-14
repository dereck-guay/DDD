using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class WanderingAIComponent : MonoBehaviour
{
    public FloatInterval delay;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public bool isStunned;

    float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            cooldown += Time.deltaTime;

            if (cooldown >= delay.Current)
            {
                cooldown -= delay.Current;
                delay.NewRandomValue();

                SetDestination();
            }
        }        
    }

    public Vector3 SetDestination()
    {
        //agent.SetDestination(Vector3.zero);
        return Vector3.zero;
    }

    public void Go()
    {
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }
}
