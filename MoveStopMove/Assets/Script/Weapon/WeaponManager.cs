using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform[] spawnPoint;
    public Hammer hammer;

    public float attackRate;
    float time;
    float spaceTime;

    public void OnInit()
    {
        time = 0;
        spaceTime = 1 / attackRate;
    }

    public void OnAttack()
    {
        time += Time.deltaTime;

        if (time >= spaceTime)
        {
            time -= spaceTime;
            Fire();
        }
    }

    public void Fire()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            //TODO: fix late
            //Instantiate(bullet, shotPoints[i].position, shotPoints[i].rotation).GetComponent<Bullet>().OnInit();

            HammerPool.Spawn<Hammer>(hammer, spawnPoint[i].position, spawnPoint[i].rotation).OnInit();
        }
    }
}
