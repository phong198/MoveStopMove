using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IStates
{
    public void OnEnter(Character target)
    {

    }

    public void OnExecute(Character target)
    {
        target.Attack();
    }

    public void OnExit(Character target)
    {
        target.StopAttack();
    }
}
