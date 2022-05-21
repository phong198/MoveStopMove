using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    private void OnEnable()
    {
        damageType = 1;
        bulletDamage = Constant.KNIFE_DAMAGE;
        bulletSpeed = 16f;
    }
}
