using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit hammer;
    [SerializeField] private Transform hammerPoolParent;
    void Start()
    {
        PoolSystem.Preload(hammer, 15, hammerPoolParent);
    }
}
