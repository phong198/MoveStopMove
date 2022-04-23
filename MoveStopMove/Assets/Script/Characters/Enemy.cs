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
    [SerializeField]
    private float idleTimer;

    [SerializeField]
    private NavMeshAgent agent;

    private double reactionTimer;
    private bool timerIsRunning;

    public override void OnEnable()
    {
        base.OnEnable();
        agent = GetComponent<NavMeshAgent>();
        idleTimerCount = 0;
    }


    public override void ChangeFromIdleToPatrol()
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
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public override void Patrol()
    {
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
        agent.isStopped = true;
    }

    public override void FindTarget()
    {
        if (AttackTargets.Count != 0)
        {
            ChangeState(new StateIdle());
        }
    }

    public override void ChangeFromIdleToAttack()
    {
        if (AttackTargets.Count != 0)
        {
            idleToAttackTimer -= Time.deltaTime;
            if (idleToAttackTimer <= 0)
            {
                ChangeState(new StateAttack());
                idleToAttackTimer = idleToAttackTime;
            }
        }
    }
}
