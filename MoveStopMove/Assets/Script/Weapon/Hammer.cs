using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    private void OnEnable()
    {
        damageType = 1;
        bulletDamage = Constant.HAMMER_DAMAGE;
        bulletSpeed = 8f;
    }
}
