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
    private float changeStateTimer = 5;
    private bool stateTimerIsRunning = false;

    public override void Start()
    {

        ChangeState(new StatePatrol());
        stateTimerIsRunning = true;
        //Run();
    }

    //internal virtual void Run()
    //{
    //    if (stateTimerIsRunning)
    //    {
    //        if (changeStateTimer > 0)
    //        {
    //            changeStateTimer -= Time.deltaTime;
    //        }
    //    }
    //    else
    //    {
    //        ChangeState(new StateIdle());
    //        changeStateTimer = 5f;
    //        stateTimerIsRunning = false;
    //    }

    //}

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(changeStateTimer);
        //Debug.Log(stateTimerIsRunning);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            ChangeState(new StateAttack());
        }    
    }

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
