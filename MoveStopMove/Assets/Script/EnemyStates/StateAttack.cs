using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IStates
{
    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExecute(Enemy enemy)
    {
        enemy.Attack();
    }

    public void OnExit(Enemy enemy)
    {

    }
}
