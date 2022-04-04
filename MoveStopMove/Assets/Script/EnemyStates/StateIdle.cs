using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IStates
{
    public void OnEnter(Character target)
    {

    }

    public void OnExecute(Character target)
    {
        target.Idle();
        target.StartIdleTimer();
    }

    public void OnExit(Character target)
    {
        target.StopIdle();
    }
}

