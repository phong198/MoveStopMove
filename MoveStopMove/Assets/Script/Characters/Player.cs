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



    private void FixedUpdate()
    {
        if (joystickUI.activeInHierarchy)
        {
            _rigid.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rigid.velocity.y, _joystick.Vertical * moveSpeed);
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigid.velocity);
                Anim.SetBool(Constant.ANIM_IDLE, false);
                ChangeState(null);
            }
            else
            {
                Anim.SetBool(Constant.ANIM_IDLE, true);
                ChangeState(new StateIdle());
            }
        }

    }

    public override void FindTarget()
    {
        if (AttackTargets.Count != 0 && _joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            ChangeState(new StateAttack());
        }
    }

    public override void Die()
    {
        base.Die();
        joystickUI.SetActive(false);
    }
}
