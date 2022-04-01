using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IStates
{
    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExecute(Enemy enemy)
    {
        //Debug.Log("Idling");
    }

    public void OnExit(Enemy enemy)
    {

    }
}

