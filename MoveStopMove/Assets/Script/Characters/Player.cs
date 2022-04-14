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


    private void FixedUpdate()
    {
        _rigid.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rigid.velocity.y, _joystick.Vertical * moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigid.velocity);
            Anim.SetBool("IsIdle", false);
        }
        else
        {
            Anim.SetBool("IsIdle", true);
        }

    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (other.gameObject.CompareTag("Character"))
        {
            if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            {
                ChangeState(new StateAttack());
                attackTarget = other.gameObject;
            }
            else
            {
                ChangeState(null);
            }
        }
    }
}
