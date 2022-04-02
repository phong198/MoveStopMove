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

    GameObject attackTarget;

    public override void Start()
    {
        base.Start();
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
        //Debug.Log("currentState:" + currentState);
    }

    public override void Patrol()
    {
        base.Patrol();
        timer += Time.deltaTime;

        {
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.isStopped = false;
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }
    public override void StopPatrol()
    {
        base.StopPatrol();
        agent.isStopped = true;
        timer = wanderTimer;
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
            attackTarget = other.gameObject;
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


    public void Idle()
    {

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
