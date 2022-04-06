using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public static UnityAction OnAttack;
    public static UnityAction OnHit;
    public static UnityAction OnDie;
    private int score;

    public Transform _transform;
    public Vector3 weapDirection;
    public Animator Anim;
    public GameObject _Character;
    protected GameObject attackTarget;

    public WeaponManager weaponManager;

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        //_Character = GetComponent<GameObject>();
    }

    private void Start()
    {
        weaponManager?.OnInit();
    }

    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Debug.Log(currentState);

        
    }

    public virtual void FindDestination()
    {

    }

    public virtual void Attack()
    {
        Anim.SetBool("IsAttack", true);
        if (attackTarget != null)
        {
            transform.LookAt(attackTarget.transform);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
        if (OnAttack != null)
        {
            OnAttack();
        }

        weaponManager?.OnAttack();
    }
    public virtual void StopAttack()
    {
        Anim.SetBool("IsAttack", false);
    }

    public virtual void Patrol()
    {

    }
    public virtual void StopPatrol()
    {

    }

    public virtual void StartIdleTimer()
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

    public virtual void OnTriggerStay(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }
}
