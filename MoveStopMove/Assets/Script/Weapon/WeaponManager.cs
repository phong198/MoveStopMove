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
            switch (owner.equipedWeapon)
            {
                case Character.Weapon.Hammer:
                    PoolSystem.Spawn<Hammer>(hammer, spawnPoint[i].position, spawnPoint[i].rotation).OnInit(owner);
                    break;
                case Character.Weapon.Knife:
                    PoolSystem.Spawn<Knife>(knife, spawnPoint[i].position, spawnPoint[i].rotation).OnInit(owner);
                    break;
                case Character.Weapon.Candy:
                    PoolSystem.Spawn<Candy>(candy, spawnPoint[i].position, spawnPoint[i].rotation).OnInit(owner);
                    break;

            }
        }
    }
}
