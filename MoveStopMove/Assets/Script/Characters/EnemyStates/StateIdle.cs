using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IStates
{
    public void OnEnter(Character target)
    {
        target.Idle();
    }

    public void OnExecute(Character target)
    {
        target.StartIdleTimer();
        target.FindTarget();
    }

    public void OnExit(Character target)
    {
        target.StopIdle();
    }
}

