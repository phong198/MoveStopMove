using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{

    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;

    public void Start()
    {
        ChangeState(new StatePatrol());
    }


    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Debug.Log("currentState:" + currentState);
    }

    public void Patrol()
    {
        timer += Time.deltaTime;

        {
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Character"))
    //    {
    //        ChangeState(new StateAttack());
    //    }    
    //}

    public void Idle()
    {

    }

    private IStates currentState;

    public void ChangeState(IStates state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
