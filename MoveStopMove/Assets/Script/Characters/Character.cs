using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Transform _transform;
    public Vector3 weapDirection;
    public Animator Anim;
    public GameObject _Character;


    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        _Character = GetComponent<GameObject>();
    }


    void Update()
    {

    }
    public virtual void Attack()
    {
        Anim.SetBool("IsAttack", true);
    }
    public virtual void StopAttack()
    {
        Anim.SetBool("IsAttack", false);
    }

    public virtual void Patrol()
    {
        //Anim.Set("IsRun")
    }
    public virtual void StopPatrol()
    {

    }

    protected IStates currentState;

    public virtual void ChangeState(IStates state)
    {
        if (currentState == state)
            return;
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
