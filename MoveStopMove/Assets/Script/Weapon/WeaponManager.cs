using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    public Hammer hammer;

    public void Fire(Character owner)
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            HammerPool.Spawn<Hammer>(hammer, spawnPoint[i].position, spawnPoint[i].rotation).OnInit(owner);
        }
    }
}
