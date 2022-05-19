using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit expSmall;
    [SerializeField] private GameUnit expBig;
    [SerializeField] private Transform expPoolParent;
    void Start()
    {
        PoolSystem.Preload(expSmall, 20, expPoolParent);
        PoolSystem.Preload(expBig, 5, expPoolParent);
    }
}
