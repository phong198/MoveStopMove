using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Vector3 pos;
    [SerializeField]
    private float wanderRadius;

    private float idleTimerCount;
    [SerializeField]
    private float idleTimer;

    [SerializeField]
    private NavMeshAgent agent;

    public GameObject TargetIcon;

    private double reactionTimer;
    private bool reactionTimerIsRunning = false;

    public override void OnEnable()
    {
        base.OnEnable();
        idleTimerCount = 0;
    }

    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }

    private void ShowTargetIcon()
    {
        if (gameObject == Player.target)
        {
            TargetIcon.SetActive(true);
        }
        else
        {
            TargetIcon.SetActive(false);
        }
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
        reactionTimer = UnityEngine.Random.Range(0.5f, 2f);
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

    public override void SetReactionTimer()
    {
        reactionTimer = UnityEngine.Random.Range(0f, 2f);
    }

    public override void Patrol()
    {
        agent.speed = moveSpeed;
        agent.isStopped = false;
        agent.SetDestination(pos);
        if (!agent.hasPath)
        {
            ChangeState(new StateIdle());
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
            reactionTimerIsRunning = true;
            StartReactiontimer();
        }
    }

    private void StartReactiontimer()
    {
        if (reactionTimerIsRunning)
        {
            reactionTimer -= Time.deltaTime;
            if (reactionTimer <= 0 && AttackTargets.Count != 0)
            {
                ChangeState(new StateIdle());
                reactionTimerIsRunning = false;
            }
        }
    }

    public override void Idle()
    {
        base.Idle();
        idleToAttackTimerIsRunning = true;
    }

    public override void ChangeFromIdleToAttack()
    {
        if (AttackTargets.Count != 0 && idleToAttackTimerIsRunning)
        {
            idleToAttackTimer -= Time.deltaTime;
            if (idleToAttackTimer <= 0)
            {
                ChangeState(new StateAttack());
                idleToAttackTimerIsRunning = false;
            }
        }
    }

    public override void DespawnWhenDie()
    {
        PoolSystem.Despawn(this);
    }

}
