using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameUnit hammer;
    [SerializeField] private Transform hammerPoolParent;
    [SerializeField] private GameUnit knife;
    [SerializeField] private Transform knifePoolParent;
    [SerializeField] private GameUnit candy;
    [SerializeField] private Transform candyPoolParent;
    void Start()
    {
        PoolSystem.Preload(hammer, 10, hammerPoolParent);
        PoolSystem.Preload(knife, 10, knifePoolParent);
        PoolSystem.Preload(candy, 10, candyPoolParent);
    }
}
