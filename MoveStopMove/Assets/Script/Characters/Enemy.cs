using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : Character
{
    public enum EnemyName { deGea, Lindelof, Bailly, Jones, Maguire, Pogba, Ronaldo, Mata, Rashford, Greenwood, Grant, Lingard, Fred, Fernandes, Varane, Dalot, Cavani, Heaton, Shaw, Sancho, Henderson, Telles, WanBissaka, Matic, Elanga, McTominay, Mejbri, Shoretire, Fernandez, Garnacho }
    private Vector3 pos;
    [SerializeField] private float wanderRadius;

    private float idleTimerCount;
    [SerializeField] private float idleTimer;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Player player;

    [SerializeField] private EnemiesSpawner spawner;

    [SerializeField] private CharacterSkins enemySkins;

    public GameObject TargetIcon;

    private double reactionTimer;
    private bool reactionTimerIsRunning = false;

    public override void OnEnable()
    {
        base.OnEnable();
        idleTimerCount = 0;
    }

    public override void Update()
    {
        base.Update();
        ShowTargetIcon();
    }

    public override void OnInit()
    {
        base.OnInit();
        characterName = ((EnemyName)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(EnemyName)).Cast<EnemyName>().Max())).ToString();

        GetDefaultClothes();
        ChangeClothes((Clothes)UnityEngine.Random.Range(0, (int)Enum.GetValues(typeof(Clothes)).Cast<Clothes>().Max()));

        //Random Level khi spawn
        for (int i = 0; i < UnityEngine.Random.Range(player.level, player.level + 2); i++)
        {
            IncreaseLevel();
        }
    }

    public override void EquipWeapon()
    {
        //random vũ khí khi spawn
        equipedWeapon = (Weapon)UnityEngine.Random.Range(0, 3);
        base.EquipWeapon();
    }

    public override void GetDefaultClothes()
    {
        base.GetDefaultClothes();
        skinRenderer.material = enemySkins.SkinColor[UnityEngine.Random.Range(0, 20)];
    }

    private void ShowTargetIcon()
    {
        if (gameObject == Player.target)
        {
            TargetIcon.SetActive(true);
        }
        else
        {
            TargetIcon.SetActive(false);
        }
    }

    public override void ChangeFromIdleToPatrol()
    {
        idleTimerCount += Time.deltaTime;
        if (idleTimerCount >= idleTimer)
        {
            ChangeState(new StatePatrol());
            idleTimerCount = 0;
        }
    }

    public override void FindDestination()
    {
        reactionTimer = UnityEngine.Random.Range(0.5f, 2f);
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        pos = newPos;
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public override void SetReactionTimer()
    {
        reactionTimer = UnityEngine.Random.Range(0f, 2f);
    }

    public override void Patrol()
    {
        agent.speed = moveSpeed;
        agent.isStopped = false;
        agent.SetDestination(pos);
        if (!agent.hasPath)
        {
            ChangeState(new StateIdle());
        }
    }

    public override void StopPatrol()
    {
        agent.isStopped = true;
    }

    public override void FindTarget()
    {
        if (AttackTargets.Count != 0)
        {
            reactionTimerIsRunning = true;
            StartReactiontimer();
        }
    }

    private void StartReactiontimer()
    {
        if (reactionTimerIsRunning)
        {
            reactionTimer -= Time.deltaTime;
            if (reactionTimer <= 0 && AttackTargets.Count != 0)
            {
                ChangeState(new StateIdle());
                reactionTimerIsRunning = false;
            }
        }
    }

    public override void Idle()
    {
        base.Idle();
        idleToAttackTimerIsRunning = true;
    }

    public override void ChangeFromIdleToAttack()
    {
        if (AttackTargets.Count != 0 && idleToAttackTimerIsRunning)
        {
            idleToAttackTimer -= Time.deltaTime;
            if (idleToAttackTimer <= 0)
            {
                ChangeState(new StateAttack());
                idleToAttackTimerIsRunning = false;
            }
        }
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        GetPerk(UnityEngine.Random.Range(1, 6));
    }

    public override void DespawnWhenDie()
    {
        base.DespawnWhenDie();
        PoolSystem.Despawn(this);
        GameFlowManager.Instance.enemiesLeftCount--;
        GameFlowManager.Instance.enemiesActiveInPool--;
        GameFlowManager.Instance.CheckGameStateWin();
        EventManager.Instance.CharacterDie();
    }

    public override void CheckWin()
    {
        if (GameFlowManager.Instance.enemiesLeftCount == 1 && player.isDead)
        {
            ChangeState(null);
            Anim.SetBool(Constant.ANIM_WIN, true);
        }
    }
}
