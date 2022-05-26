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
        PoolSystem.Preload(hammer, 20, hammerPoolParent);
        PoolSystem.Preload(knife, 20, knifePoolParent);
        PoolSystem.Preload(candy, 20, candyPoolParent);
    }
}
