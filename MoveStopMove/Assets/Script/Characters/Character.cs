using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Character : MonoBehaviour
{
    public WeaponManager weaponManager;

    protected Animator Anim;
    public List<GameObject> AttackTargets = new List<GameObject>();

    protected int score;

    protected float fireTime = 0.16f;
    protected float fireTimer;
    protected float attackToIdleTime = 2.05f;
    protected float attackToIdleTimer;
    protected float idleToAttackTime = 0.5f;
    protected float idleToAttackTimer;

    protected bool isFired;

    [SerializeField]
    protected bool isDead;

    public virtual void Awake()
    {
        Anim = GetComponent<Animator>();
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

        RemoveDeadTargets();
    }

    #region Patrol
    //Start Patrol Region

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

    //End Patrol Region
    #endregion

    #region Idle
    //Start Idle Region

    public virtual void Idle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, true);
        idleToAttackTimer = idleToAttackTime;
    }

    public virtual void ChangeFromIdleToPatrol()
    {
        
    }

    public virtual void StopIdle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, false);
    }

    public virtual void ChangeFromIdleToAttack()
    {

    }

    //End Idle Region
    #endregion

    #region Die
    //Start Die Region

    public virtual async void Die()
    {
        isDead = true;
        AttackTargets.Clear();
        ChangeState(null);
        Anim.SetBool(Constant.ANIM_DIE, true);

        await Task.Delay(TimeSpan.FromSeconds(2));
        DespawnWhenDie();
    }

    public virtual void DespawnWhenDie()
    {
        EnemyPool.PoolAccess.DespawnFromPool(gameObject);
    }

    //End Die Region
    #endregion

    #region Attack
    //Start Attack Region

    public virtual void Attack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, true);
        isFired = true;

        fireTimer = fireTime;
        attackToIdleTimer = attackToIdleTime;
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

    public virtual void RemoveDeadTargets()
    {
        AttackTargets.RemoveAll(attackTarget => attackTarget.GetComponent<Character>().isDead);
    }

    public virtual void StopAttack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, false);
    }

    //End Attack Region
    #endregion

    public virtual void LookAtTarget()
    {
        if (AttackTargets.Count != 0)
        {
            transform.LookAt(AttackTargets[0].transform);
        }
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
