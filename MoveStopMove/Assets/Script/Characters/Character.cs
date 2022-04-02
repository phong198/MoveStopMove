using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Transform _transform;
    public Vector3 weapDirection;
    public Animator Anim; 
    public GameObject _Character { get; private set; }
    [SerializeField]
    private void Awake()
    {
        Anim = GetComponent<Animator>();
        _Character = GetComponent<GameObject>();
    }
    public virtual void Start()
    {
        
    }


    void Update()
    {

    }

    public void Attack()
    {
        Anim.SetBool("IsIdle", true);
    }

}
