using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{

    public Animator Anim { get; private set; }
    public GameObject _Character { get; private set; }
    [SerializeField]

    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        _Character = GetComponent<GameObject>(); 
    }


    void Update()
    {
        //Debug.Log(Anim.GetBool());
    }

    public void Attack()
    {
        //Anim.SetBool("IsAttack", true);
    }

}
