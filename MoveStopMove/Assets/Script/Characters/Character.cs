using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Character : MonoBehaviour
{
    public WeaponManager weaponManager;

    protected Animator Anim;
    public List<GameObject> AttackTargets = new List<GameObject>();
    [SerializeField]
    protected SphereCollider _collider;
    protected int score;

    protected float fireTime = 1f;
    protected float fireTimer;
    protected float attackToIdleTime = 2.05f;
    protected float attackToIdleTimer;

    protected bool isFired;

    protected bool isDead;

    protected float attackRange;
    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
        attackRange = _collider.radius;
        fireTimer = fireTime;
        attackToIdleTimer = attackToIdleTime;
    }

    public virtual void OnEnable()
    {
        isDead = false;
        ChangeState(new StateIdle());
    }

    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Debug.Log("fireTimer: " + fireTimer);
        Debug.Log("fireTime: " + fireTime);
        Debug.Log("attackToIdleTimer: " + attackToIdleTimer);
        Debug.Log("attackToIdleTime: " + attackToIdleTime);
    }

    public virtual void FindDestination()
    {

    }  

    public virtual void Patrol()
    {

    }
    public virtual void StopPatrol()
    {

    }

    public virtual void FindTarget()
    {

    }

    public virtual void Idle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, true);
    }

    public virtual void StartIdleTimer()
    {
        
    }

    public virtual void StopIdle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, false);
    }

    public virtual void Die()
    {
        isDead = true;
        AttackTargets.Clear();
        ChangeState(null);
        Anim.SetBool(Constant.ANIM_DIE, true);
        Invoke("DespawnWhenDie", 2f);
    }

    public virtual void DespawnWhenDie()
    {
        EnemyPool.PoolAccess.DespawnFromPool(gameObject);
    }

    public virtual void CheckTargetList()
    {
        foreach (GameObject attacktarget in AttackTargets.Reverse<GameObject>())
        {
            if (attacktarget.GetComponent<Character>().isDead)
            {
                AttackTargets.Remove(attacktarget);
            }
        }
    }

    public virtual void Attack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, true);
        isFired = true;
        transform.LookAt(AttackTargets[0].transform);
    }

    public virtual void StartFireTimer()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0 && isFired)
        {
            weaponManager.Fire();
            isFired = false;
            fireTimer = fireTime;
        }
    }

    public virtual void ChangeFromAttackToIdle()
    {
        attackToIdleTimer -= Time.deltaTime;
        if (attackToIdleTimer <= 0)
        {
            ChangeState(new StateIdle());
            attackToIdleTimer = attackToIdleTime;
        }
    }

    public virtual void StopAttack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, false);
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(Constant.TAG_WEAPON))
        {
            Die();
        }                 
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            AttackTargets.Add(other.gameObject);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            AttackTargets.Remove(other.gameObject);
        }
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
