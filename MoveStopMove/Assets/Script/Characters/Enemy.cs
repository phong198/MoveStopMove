using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Vector3 pos;

    public float wanderRadius;

    private float timerCount;
    public float wanderTimer;

    private float idleTimerCount;
    public float idleTimer;

    private NavMeshAgent agent;

    private double reactionTimer;


    GameObject attackTarget;

    public override void Start()
    {
        base.Start();
        ChangeState(new StatePatrol());
    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timerCount = 0;
        idleTimerCount = 0;
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Debug.Log(currentState);
    }

    public override void StartIdleTimer()
    {
        timerCount += Time.deltaTime;
        if (timerCount >= wanderTimer)
        {
            ChangeState(new StateIdle());
            timerCount = 0;
        }
        Debug.Log("idleTimer: " + idleTimer);
        Debug.Log("idleTimerCount: " + idleTimerCount);
    }


    public override void StartPatrolTimer()
    {
        idleTimerCount += Time.deltaTime;
        if (idleTimerCount >= idleTimer)
        {
            ChangeState(new StatePatrol());
            idleTimerCount = 0;
        }
        Debug.Log("wanderTimer: " + wanderTimer);
        Debug.Log("timerCount: " + timerCount);
    }

    public override void Patrol()
    {
        base.Patrol();

        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.isStopped = false;
        agent.SetDestination(newPos);
        pos = newPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pos, 2);
    }

    public override void StopPatrol()
    {
        base.StopPatrol();
        agent.isStopped = true;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
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

    private void OnTriggerStay(Collider other)
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            ChangeState(new StatePatrol());
            attackTarget = null;
        }
    }


    public override void Attack()
    {
        base.Attack();
        transform.LookAt(attackTarget.transform);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
