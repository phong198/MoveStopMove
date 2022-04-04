using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IStates
{

    public void OnEnter(Character target)
    {
        target.FindDestination();
    }

    public void OnExecute(Character target)
    {
        target.Patrol();
    }

    public void OnExit(Character target)
    {
        target.StopPatrol();
    }
}
