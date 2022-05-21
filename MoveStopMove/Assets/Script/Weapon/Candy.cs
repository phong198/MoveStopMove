using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Weapon
{
    private void OnEnable()
    {
        damageType = 2;
        bulletDamage = Constant.CANDY_DAMAGE;
        bulletSpeed = 8f;
    }
}
