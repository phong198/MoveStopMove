using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Transform _transform;
    public Vector3 weapDirection;
    public Animator Anim;
    public GameObject _Character;


    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        _Character = GetComponent<GameObject>();
    }


    void Update()
    {

    }

    public void Attack()
    {
        Anim.SetBool("IsIdle", true);
    }

}
