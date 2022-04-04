using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IStates
{
    bool isPatrol = false;


    public void OnEnter(Character target)
    {

    }


    public void OnExecute(Character target)
    {
        target.Patrol();
        target.StartIdleTimer();
    }

    public void OnExit(Character target)
    {
        target.StopPatrol();
    }
}
