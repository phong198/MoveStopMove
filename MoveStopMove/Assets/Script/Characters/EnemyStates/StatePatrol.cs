using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : IStates
{
    public void OnEnter(Character target)
    {
        target.FindDestination();
        target.SetReactionTimer();
    }

    public void OnExecute(Character target)
    {
        target.Patrol();
        target.FindTarget();
    }

    public void OnExit(Character target)
    {
        target.StopPatrol();
    }
}
