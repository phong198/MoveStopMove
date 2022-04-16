using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    //public static UnityAction OnAttack;
    //public static UnityAction OnHit;
    //public static UnityAction OnDie;
    private int score;
    public Animator Anim;
    protected GameObject attackTarget;
    public WeaponManager weaponManager;
    public bool inAttackRange;


    protected float attackRange;
    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        weaponManager?.OnInit();
        attackRange = transform.Find("AttackRange").GetComponent<SphereCollider>().radius;
    }

    private void Start()
    {

    }
    
    public virtual void OnEnable()
    {
     
    }

    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }     
    }

    public virtual void FindDestination()
    {

    }

    public virtual void Die()
    {
        ChangeState(null);
        Anim.SetBool("IsDead", true);
        Invoke("DespawnWhenDie", 2f);
    }

    public virtual void DespawnWhenDie()
    {
        EnemyPool.PoolAccess.DespawnFromPool(gameObject);   
    }    
    public virtual void Attack()
    {
        float targetDistance = (attackTarget.transform.position - transform.position).magnitude;

        if ((attackTarget != null) && attackTarget.activeInHierarchy && (targetDistance <= attackRange))
        {
            Anim.SetBool("IsAttack", true);
            transform.LookAt(attackTarget.transform);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;
            transform.rotation = Quaternion.Euler(eulerAngles);
            weaponManager?.OnAttack();
        }
        else
        {
            ChangeState(new StateIdle());
        }        
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

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            Die();
        }      
            
    }

    public virtual void OnTriggerStay(Collider other)
    {

    }

    public virtual void OnTriggerExit(Collider other)
    {

    }
}
