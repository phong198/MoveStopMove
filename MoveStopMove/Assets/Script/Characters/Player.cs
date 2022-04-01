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
    public Camera _camera;
    [SerializeField]
    private Vector3 offset;
    public GameObject weapon;


    private void FixedUpdate()
    {
        _rigid.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _rigid.velocity.y, _joystick.Vertical * moveSpeed);
        _camera.transform.position = gameObject.transform.position + offset;
        if (_joystick.Horizontal != 0 || _joystick.Vertical !=0)
        {
            transform.rotation = Quaternion.LookRotation(_rigid.velocity);
            Anim.SetBool("IsIdle", false);
        }    
        else
        {
            Anim.SetBool("IsIdle", true);
        }

    }
}
