using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GameUnit
{
    private void OnEnable()
    {
        bulletID = 3;
        bulletSpeed = 8f;
    }
}
