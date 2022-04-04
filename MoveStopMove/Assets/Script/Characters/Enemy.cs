using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Vector3 pos;

    public float wanderRadius;


    private float idleTimerCount;
    public float idleTimer;

    private NavMeshAgent agent;

    private double reactionTimer;


    private void Start()
    {
        ChangeState(new StatePatrol());
    }

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        idleTimerCount = 0;
    }

    public override void StartIdleTimer()
    {
        idleTimerCount += Time.deltaTime;
        if (idleTimerCount >= idleTimer)
        {
            ChangeState(new StatePatrol());
            idleTimerCount = 0;
        }
    }

    public override void FindDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        pos = newPos;
    }


    public override void Patrol()
    {
        base.Patrol();
        {
            agent.isStopped = false;
            agent.SetDestination(pos);
            if (!agent.hasPath)
            {
                ChangeState(new StateIdle());
            }
        }
    }

    public override void StopPatrol()
    {
        base.StopPatrol();
        agent.isStopped = true;
    }

    private static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            System.Random rng = new System.Random();
            reactionTimer = (rng.NextDouble() * 2f);
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Character"))
            {
                if (reactionTimer > 0)
                {
                    reactionTimer -= Time.deltaTime;
                }
                else
                {
                    ChangeState(new StateAttack());
                    attackTarget = other.gameObject;
                }
            }
        }
    }
    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            ChangeState(new StatePatrol());
            attackTarget = null;
        }
    }
}
