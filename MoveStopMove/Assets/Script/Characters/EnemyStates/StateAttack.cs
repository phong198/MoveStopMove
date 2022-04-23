using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IStates
{
    public void OnEnter(Character target)
    {
        target.Attack();
    }

    public void OnExecute(Character target)
    {
        target.LookAtTarget();
        target.StartFireTimer();
        target.ChangeFromAttackToIdle();
        target.RemoveDeadTargets();
    }

    public void OnExit(Character target)
    {
        target.StopAttack();
    }
}
