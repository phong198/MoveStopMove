using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Character : GameUnit, IBoost, IDamage
{
    public SphereCollider attackRangeCollider;
    [HideInInspector] public float attackRadius;
    public WeaponManager weaponManager;

    [HideInInspector] public string characterName;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public int characterDamage;
    [HideInInspector] public int characterDamageBeforePerks;
    [HideInInspector] public int exp;
    [HideInInspector] public int expToNextLevel;
    [HideInInspector] public int level;

    public enum Weapon { Hammer, Knife, Candy }
    public Weapon equipedWeapon;
    [SerializeField] protected GameObject weaponHolder;
    [SerializeField] protected GameObject hammerInHand;
    [SerializeField] protected GameObject knifeInHand;
    [SerializeField] protected GameObject candyInHand;
    protected GameObject currentWeaponInHand;
    protected GameObject lastWeaponInHand;


    [SerializeField] protected Animator Anim;
    [SerializeField] protected List<GameObject> AttackTargets = new List<GameObject>();
    [HideInInspector] public Character currentAttacker;
    protected Character burnAttacker;

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
    [HideInInspector] public bool isDead;

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
    [HideInInspector] public int spawnpointID;

    [SerializeField] protected GameObject characterCanvas;

    [SerializeField] protected Transform headPosition;
    [SerializeField] protected Transform backPosition;
    [SerializeField] protected Transform freeHandPosition;
    [SerializeField] protected Transform tailPosition;
    [SerializeField] protected ClothesInfo characterClothes;
    [SerializeField] protected SkinnedMeshRenderer pantRenderer;
    [SerializeField] protected SkinnedMeshRenderer skinRenderer;
    [HideInInspector] public enum Clothes { Default, Arrow, Cowboy, Crown, Ear, Hat, Hat_Cap, Hat_Yellow, HeadPhone, Rau, Khien, Shield, Batman, Chambi, comy, dabao, onion, pokemon, rainbow, Skull, Vantim, Devil, Angel, Witch, Deadpool, Thor }
    public virtual void OnEnable()
    {
        //TODO: chuyển OnInit ra chỗ khác
        OnInit();
        EventManager.Instance.onCharacterDie += CheckWin;
    }
    public virtual void OnDisable()
    {
        EventManager.Instance.onCharacterDie -= CheckWin;
    }

    public virtual void OnInit()
    {
        characterCanvas.SetActive(false);
        moveSpeed = 5f;
        maxHealth = 10;
        currentHealth = maxHealth;
        characterDamage = 5;
        characterDamageBeforePerks = characterDamage;
        exp = 0;
        expToNextLevel = 10;
        level = 1;
        isDead = false;
        attackRadius = attackRangeCollider.radius;
        selectedWeaponSpawnPoint = weaponSpawnPoint1;
        EquipWeapon();
        ChangeState(new StateIdle());
    }

    public virtual void Update()
    {
        if (GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameStart)
        {
            characterCanvas.SetActive(true);
        }
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

    #region Equip Clothes
    //Start Equip Clothes region
    public virtual void ChangeClothes(Clothes clothes)
    {
        switch (clothes)
        {
            case Clothes.Default: //default skin
                GetDefaultClothes();
                break;
            case Clothes.Arrow: //mũ
                ResetHead();
                Instantiate(characterClothes.HeadPosition[6], headPosition);
                break;
            case Clothes.Cowboy:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[7], headPosition);
                break;
            case Clothes.Crown:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[1], headPosition);
                break;
            case Clothes.Ear:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[2], headPosition);
                break;
            case Clothes.Hat:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[3], headPosition);
                break;
            case Clothes.Hat_Cap:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[4], headPosition);
                break;
            case Clothes.Hat_Yellow:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[5], headPosition);
                break;
            case Clothes.HeadPhone:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[8], headPosition);
                break;
            case Clothes.Rau:
                ResetHead();
                Instantiate(characterClothes.HeadPosition[0], headPosition);
                break;
            case Clothes.Khien: //khiên
                ResetFreeHand();
                Instantiate(characterClothes.FreeHandPosition[1], freeHandPosition);
                break;
            case Clothes.Shield:
                ResetFreeHand();
                Instantiate(characterClothes.FreeHandPosition[0], freeHandPosition);
                break;
            case Clothes.Batman: //quần
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[7];
                break;
            case Clothes.Chambi:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[1];
                break;
            case Clothes.comy:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[2];
                break;
            case Clothes.dabao:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[3];
                break;
            case Clothes.onion:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[4];
                break;
            case Clothes.rainbow:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[5];
                break;
            case Clothes.pokemon:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[8];
                break;
            case Clothes.Skull:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[9];
                break;
            case Clothes.Vantim:
                GetDefaultClothes();
                pantRenderer.material = characterClothes.PantsMaterials[6];
                break;
            case Clothes.Devil: //skin
                ResetClothes();
                Instantiate(characterClothes.HeadPosition[9], headPosition);
                Instantiate(characterClothes.BackPosition[0], backPosition);
                Instantiate(characterClothes.TailPosition[0], tailPosition);
                skinRenderer.material = characterClothes.SkinMaterials[1];
                break;
            case Clothes.Angel:
                ResetClothes();
                Instantiate(characterClothes.HeadPosition[10], headPosition);
                Instantiate(characterClothes.BackPosition[1], backPosition);
                Instantiate(characterClothes.FreeHandPosition[2], freeHandPosition);
                skinRenderer.material = characterClothes.SkinMaterials[2];
                break;
            case Clothes.Witch:
                ResetClothes();
                Instantiate(characterClothes.HeadPosition[11], headPosition);
                Instantiate(characterClothes.FreeHandPosition[3], freeHandPosition);
                skinRenderer.material = characterClothes.SkinMaterials[3];
                break;
            case Clothes.Deadpool:
                ResetClothes();
                Instantiate(characterClothes.BackPosition[2], backPosition);
                skinRenderer.material = characterClothes.SkinMaterials[4];
                break;
            case Clothes.Thor:
                ResetClothes();
                Instantiate(characterClothes.HeadPosition[12], headPosition);
                skinRenderer.material = characterClothes.SkinMaterials[5];
                break;
        }
    }

    public virtual void GetDefaultClothes()
    {
        
    }

    public virtual void ResetClothes()
    {
        ResetFreeHand();
        ResetHead();
        ResetBack();
        ResetTail();
        GetDefaultClothes();
    }

    public virtual void ResetFreeHand()
    {
        foreach (Transform item in freeHandPosition)
        {
            Destroy(item.gameObject);
        }
    }

    public virtual void ResetHead()
    {
        foreach (Transform item in headPosition)
        {
            Destroy(item.gameObject);
        }
    }

    public virtual void ResetBack()
    {
        foreach (Transform item in backPosition)
        {
            Destroy(item.gameObject);
        }
    }

    public virtual void ResetTail()
    {
        foreach (Transform item in tailPosition)
        {
            Destroy(item.gameObject);
        }
    }
    #endregion

    #region Equip Weapon
    //Start Equip Weapon Region
    public virtual void EquipWeapon()
    {
        ShowWeaponInHand(equipedWeapon);
    }

    public virtual void ShowWeaponInHand(Weapon equipedWeapon)
    {
        if (currentWeaponInHand != null)
        {
            lastWeaponInHand = currentWeaponInHand;
            lastWeaponInHand.SetActive(false);
        }

        switch (equipedWeapon)
        {
            case Weapon.Hammer:
                currentWeaponInHand = hammerInHand;
                break;
            case Weapon.Knife:
                currentWeaponInHand = knifeInHand;
                break;
            case Weapon.Candy:
                currentWeaponInHand = candyInHand;
                break;
        }
        currentWeaponInHand.SetActive(true);
    }
    //End Equip Weapon Region
    #endregion

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
    public virtual void Damage(int damageType, int bulletDamage, int characterDamage, Character attacker)
    {
        switch (damageType)
        {
            case 1: //dam thường
                currentAttacker = attacker;
                currentHealth -= bulletDamage + characterDamage;
                CheckDie();
                break;

            case 2: //dam cháy
                currentAttacker = attacker;
                burnAttacker = attacker;
                currentHealth -= bulletDamage + characterDamage;
                CheckDie();
                burnParticle.Play();
                StartCoroutine(Burn());

                IEnumerator Burn()
                {
                    for (int i = 0; i < Constant.BURN_DURATION;)
                    {
                        yield return new WaitForSeconds(1);
                        if (!isDead)
                        {
                            currentAttacker = burnAttacker;
                            currentHealth -= Constant.BURN_DAMAGE;
                            CheckDie();
                        }
                        ++i;
                    }
                    burnParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;
        }
    }

    public virtual void CheckDie()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (!isDead)
            {
                currentAttacker.IncreaseXP(3, level);
                Die();
            }
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
                currentHealth += Constant.BOOST_HEALTH;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                healParticle.Play();
                break;

            case 2: //boost damage
                int lastCharacterDamage = characterDamage;
                characterDamage *= Constant.BOOST_DAMAGE;
                damageBoostParticle.Play();
                StartCoroutine(BoostDamage());
                IEnumerator BoostDamage()
                {
                    yield return new WaitForSeconds(5);
                    characterDamage = lastCharacterDamage;
                    damageBoostParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                break;

            case 3: //boost speed
                float lastMoveSpeed = moveSpeed;
                moveSpeed *= Constant.BOOST_SPEED;
                speedBoostParticle.Play();
                StartCoroutine(BoostSpeed());
                IEnumerator BoostSpeed()
                {
                    yield return new WaitForSeconds(5);
                    moveSpeed = lastMoveSpeed;
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
        switch (expID)
        {
            case 1: //nhặt cục xp nhỏ
                exp += Constant.EXP_SMALL;
                break;

            case 2: //nhặt cục xp to
                exp += Constant.EXP_BIG;
                break;

            case 3: //giết được một thằng
                exp += Constant.EXP_KILL * enemyLevel;
                break;
        }

        if (exp >= expToNextLevel)
        {
            levelUpParticle.Play();
            IncreaseLevel();
        }
    }

    public virtual void IncreaseLevel()
    {
        level += 1;
        currentHealth += Constant.HEALTH_LVUP;
        maxHealth += Constant.HEALTH_LVUP;
        exp = 0;
        expToNextLevel += Constant.EXP_LVUP;
    }
    //End IncreaseXP Region
    #endregion

    #region Perks
    //Start Perks Region
    public virtual void GetPerk(int perkID)
    {
        switch (perkID)
        {
            case 1: //+ dam
                characterDamage += Constant.PERK_INCREASE_DAMAGE;
                characterDamageBeforePerks += Constant.PERK_INCREASE_DAMAGE;
                break;
            case 2: //+ máu
                currentHealth += Constant.PERK_INCREASE_HEALTH;
                maxHealth += Constant.PERK_INCREASE_HEALTH;
                break;
            case 3: //+ speed
                moveSpeed += Constant.PERK_INCREASE_SPEED;
                break;
            case 4: //ném 2 đạn thắng
                characterDamage = characterDamageBeforePerks;
                selectedWeaponSpawnPoint = weaponSpawnPoint2;
                characterDamage /= 2;
                break;
            case 5: //ném 3 đạn chéo
                characterDamage = characterDamageBeforePerks;
                selectedWeaponSpawnPoint = weaponSpawnPoint3;
                characterDamage /= 3;
                break;
            case 6: //ném 1 đạn phía sau
                characterDamage = characterDamageBeforePerks;
                selectedWeaponSpawnPoint = weaponSpawnPoint4;
                characterDamage /= 2;
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

    public virtual void CheckWin() { }

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
