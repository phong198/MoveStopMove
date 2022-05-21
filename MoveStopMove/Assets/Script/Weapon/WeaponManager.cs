using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Hammer hammer;
    public Knife knife;
    public Candy candy;

    public void Fire(Character owner, Transform[] spawnPoint)
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            PoolSystem.Spawn<Hammer>(hammer, spawnPoint[i].position, spawnPoint[i].rotation).OnInit(owner);
        }
    }
}
