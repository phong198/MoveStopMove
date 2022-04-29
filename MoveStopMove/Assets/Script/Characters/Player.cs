using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private FloatingJoystick _joystick;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject joystickUI;

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
        if (joystickUI.activeInHierarchy)
        {
            _rigid.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rigid.velocity.y, _joystick.Vertical * moveSpeed);
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigid.velocity);
                ChangeState(null);
            }
            else if (!isAttacking)
            {
                ChangeState(new StateIdle());
            }
        }
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
        joystickUI.SetActive(false);
    }

    public override void DespawnWhenDie()
    {
        gameObject.SetActive(false);
    }
}
