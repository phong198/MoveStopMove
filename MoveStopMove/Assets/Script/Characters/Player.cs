using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private GameObject joystickUI;
    [SerializeField] private GameObject perkUI;

    private bool isAttacking = false;

    public static GameObject target;

    public override void Update()
    {
        base.Update();
        if (AttackTargets.Count != 0)
        {
            target = AttackTargets[0];
        }
        else target = null;
    }

    private void FixedUpdate()
    {
        Vector3 JoystickDirection = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);

        if (joystickUI.activeInHierarchy)
        {
            transform.position += JoystickDirection * moveSpeed * Time.deltaTime;
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(JoystickDirection, transform.up);
                ChangeState(null);
            }
            else if (!isAttacking)
            {
                ChangeState(new StateIdle());
            }
        }
    }

    public override void EquipWeapon()
    {
        equipedWeapon = Weapon.Hammer;
        base.EquipWeapon();
    }

    public override void ChangeFromIdleToAttack()
    {
        if (AttackTargets.Count != 0 && _joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            ChangeState(new StateAttack());
            isAttacking = true;
        }
    }

    public override void StopAttack()
    {
        base.StopAttack();
        isAttacking = false;
    }


    public override void Die()
    {
        base.Die();
        CloseUI();
        GameFlowManager.Instance.gameState = GameFlowManager.GameState.gameOver;
    }

    public override void CheckWin()
    {
        if (GameFlowManager.Instance.gameState == GameFlowManager.GameState.gameWin)
        {
            CloseUI();
            Anim.SetBool(Constant.ANIM_WIN, true);
        }
    }

    private void CloseUI()
    {
        joystickUI.SetActive(false);
        if (perkUI.activeSelf)
        {
            perkUI.SetActive(false);
        }
    }

    public override void DespawnWhenDie()
    {
        base.DespawnWhenDie();
        gameObject.SetActive(false);
        EventManager.Instance.CharacterDie();
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        perkUI.SetActive(true);
    }
}
