using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public UnityAction OnHit;
    public UnityAction OnDie;
    
    public Transform _transform;
    public Vector3 weapDirection;
    public Animator Anim;
    public GameObject _Character;

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        _Character = GetComponent<GameObject>();
    }
    public virtual void Start()
    {
        
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

    public virtual void StartPatrol()
    {

    }

    public virtual void Patrol()
    {
        
    }
    public virtual void StopPatrol()
    {

    }

    public virtual void StartIdle()
    {

    }

    public virtual void Idle()
    {
        Anim.SetBool("IsIdle", true);
    }

    public virtual void StopIdle()
    {
        Anim.SetBool("IsIdle", false);
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
