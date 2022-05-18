﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Character : MonoBehaviour, IBoost, IDamage
{
    public SphereCollider attackRangeCollider;
    public float attackRadius;
    public WeaponManager weaponManager;
    public GameObject weaponHolder;

    public float moveSpeed;
    public int health;
    public int maxHealth;
    public int characterDamage;
    public int exp;
    public int expToNextLevel;
    public int level;

    [SerializeField] protected Animator Anim;
    [SerializeField] protected List<GameObject> AttackTargets = new List<GameObject>();

    [SerializeField] protected Transform model;
    [SerializeField] protected Transform attackRange;

    public int score;

    protected float fireTime = 0.50f;
    protected float fireTimer;
    protected float attackToIdleTime = 2.05f;
    protected float attackToIdleTimer;
    protected float idleToAttackTime = 0.5f;
    protected float idleToAttackTimer;

    protected bool fireTimerIsRunning = false;
    protected bool attackToIdleTimerIsRunning = false;
    protected bool idleToAttackTimerIsRunning = false;

    protected bool isFired;
    public bool isDead;

    [SerializeField] protected ParticleSystem healParticle;
    [SerializeField] protected ParticleSystem speedBoostParticle;
    [SerializeField] protected ParticleSystem damageBoostParticle;
    [SerializeField] protected ParticleSystem burnParticle;
    [SerializeField] protected ParticleSystem levelUpParticle;

    protected Transform[] selectedWeaponSpawnPoint;
    [SerializeField] protected Transform[] weaponSpawnPoint1;
    [SerializeField] protected Transform[] weaponSpawnPoint2;
    [SerializeField] protected Transform[] weaponSpawnPoint3;
    [SerializeField] protected Transform[] weaponSpawnPoint4;

    public virtual void OnEnable()
    {
        moveSpeed = 5f;
        health = 1000;
        characterDamage = 5;
        exp = 0;
        expToNextLevel = 10;
        level = 1;
        isDead = false;
        attackRadius = attackRangeCollider.radius;
        ChangeState(new StateIdle());
        selectedWeaponSpawnPoint = weaponSpawnPoint1;
    }

    //public virtual void OnInit()
    //{
    //    isDead = false;
    //    ChangeState(new StateIdle());
    //}

    public virtual void Update()
    {
        if (GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameStart)
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }

        if (AttackTargets.Count != 0)
        {
            RemoveDeadTargets();
        }

        if (health <= 0)
        {
            health = 0;
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

    #region Patrol
    //Start Patrol Region

    public virtual void FindDestination() { }

    public virtual void SetReactionTimer() { }

    public virtual void Patrol() { }

    public virtual void FindTarget() { }

    public virtual void StopPatrol() { }

    //End Patrol Region
    #endregion

    #region Idle
    //Start Idle Region

    public virtual void Idle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, true);
        idleToAttackTimer = idleToAttackTime;
    }

    public virtual void ChangeFromIdleToPatrol() { }

    public virtual void StopIdle()
    {
        Anim.SetBool(Constant.ANIM_IDLE, false);
    }

    public virtual void ChangeFromIdleToAttack() { }

    //End Idle Region
    #endregion

    #region Hit
    //Start Hit Region

    public virtual void Hit()
    {

    }

    //End Hit Region
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

    public virtual void DespawnWhenDie() { }

    //End Die Region
    #endregion

    #region Attack
    //Start Attack Region

    public virtual void Attack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, true);
        isFired = true;
        fireTimerIsRunning = true;
        attackToIdleTimerIsRunning = true;

        fireTimer = fireTime;
        attackToIdleTimer = attackToIdleTime;
    }

    public virtual void StartFireTimer()
    {
        if (fireTimerIsRunning)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0 && isFired)
            {
                weaponHolder.SetActive(false);
                weaponManager.Fire(gameObject.GetComponent<Character>(), selectedWeaponSpawnPoint);
                isFired = false;
                fireTimerIsRunning = false;
            }
        }
    }

    public virtual void ChangeFromAttackToIdle()
    {
        if (attackToIdleTimerIsRunning)
        {
            attackToIdleTimer -= Time.deltaTime;
            if (attackToIdleTimer <= 0)
            {
                ChangeState(new StateIdle());
                attackToIdleTimerIsRunning = false;
            }
        }
    }

    public virtual void StopAttack()
    {
        Anim.SetBool(Constant.ANIM_ATTACK, false);
        weaponHolder.SetActive(true);
    }

    //End Attack Region
    #endregion

    #region Take Damage
    //Start Take Damage Reagion
    public virtual void Damage(int BulletID, int enemyDamage)
    {
        switch (BulletID)
        {
            case 1: //búa
                health = health - Constant.HAMMER_DAMAGE - enemyDamage;
                break;

            case 2: //dao
                health = health - Constant.KNIFE_DAMAGE - enemyDamage;
                break;

            case 3: //kẹo
                health = health - Constant.CANDY_DAMAGE - enemyDamage;
                burnParticle.Play();
                StartCoroutine(Burn());
                IEnumerator Burn()
                {
                    for (int i = 0; i < Constant.CANDY_BURN_DURATION;)
                    {
                        yield return new WaitForSeconds(1);
                        health = health - Constant.CANDY_BURN_DAMAGE;
                        i++;
                    }
                    burnParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;
        }
    }
    //End Take Damage Region
    #endregion

    #region Boost
    //Start Boost Region
    public virtual void Boost(int boostID)
    {
        switch (boostID)
        {
            case 1: //boost health
                health = health + Constant.BOOST_HEALTH;
                healParticle.Play();
                break;

            case 2: //boost damage
                int oldCharacterDamage = characterDamage;
                characterDamage = characterDamage * Constant.BOOST_DAMAGE;
                damageBoostParticle.Play();
                StartCoroutine(BoostDamage());
                IEnumerator BoostDamage()
                {
                    yield return new WaitForSeconds(5);
                    characterDamage = oldCharacterDamage;
                    damageBoostParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;

            case 3: //boost speed
                float oldMoveSpeed = moveSpeed;
                moveSpeed = moveSpeed * Constant.BOOST_SPEED;
                speedBoostParticle.Play();
                StartCoroutine(BoostSpeed());
                IEnumerator BoostSpeed()
                {
                    yield return new WaitForSeconds(5);
                    moveSpeed = oldMoveSpeed;
                    speedBoostParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;
        }
    }
    //End Boost Region
    #endregion

    #region IncreaseXP
    //Start IncreaseXP Region
    public virtual void IncreaseXP(int expID, int enemyLevel)
    {
        switch(expID)
        {
            case 1: //nhặt cục xp nhỏ
                exp = exp + Constant.EXP_SMALL;
                break;

            case 2: //nhặt cục xp to
                exp = exp + Constant.EXP_BIG;
                break;

            case 3: //giết được một thằng
                exp = exp + Constant.EXP_KILL * enemyLevel;
                break;
        }
        
        if(exp >= expToNextLevel)
        {
            IncreaseLevel();
        }
    }

    public virtual void IncreaseLevel()
    {
        levelUpParticle.Play();
        level = level + 1;
        health = health + Constant.HEALTH_LVUP;
        exp = 0;
        expToNextLevel = expToNextLevel + Constant.EXP_LVUP;
    }
    //End IncreaseXP Region
    #endregion

    #region Perks
    //Start Perks Region
    public virtual void GetPerk(int perkID)
    {
        switch(perkID)
        {
            case 1: //+ dam
                characterDamage = characterDamage + Constant.PERK_INCREASE_DAMAGE;
                break;
            case 2: //+ máu
                health = health + Constant.PERK_INCREASE_HEALTH;
                break;
            case 3: //+ speed
                moveSpeed = moveSpeed + Constant.PERK_INCREASE_SPEED;
                break;
            case 4: //ném 2 đạn thắng
                selectedWeaponSpawnPoint = weaponSpawnPoint2;
                break;
            case 5: //ném 3 đạn chéo
                selectedWeaponSpawnPoint = weaponSpawnPoint3;
                break;
            case 6: //ném 1 đạn phía sau
                selectedWeaponSpawnPoint = weaponSpawnPoint4;
                break;
        }
    }

    //End Perks Region
    #endregion

    public virtual void RemoveDeadTargets()
    {
        AttackTargets.RemoveAll(attackTarget => attackTarget.GetComponent<Character>().isDead);
    }

    public virtual void LookAtTarget()
    {
        if (AttackTargets.Count != 0)
        {
            transform.LookAt(AttackTargets[0].transform);
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
