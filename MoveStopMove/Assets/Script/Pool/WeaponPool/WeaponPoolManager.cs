using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoolManager : MonoBehaviour
{
    public Transform tf_hammer;
    public Transform tf_boomerang;
    public Hammer hammer;
    //public Boomerang boomerang;

    //public Transform tf_VFX;
    //public ParticleSystem hitVFX;

    // Start is called before the first frame update
    void Awake()
    {
        //SimplePool.Preload(bullet_1, 5, tf_bullet_1);
        WeaponPool.Preload(hammer, 11, tf_hammer);

        //ParticlePool.Preload(hitVFX, 10, tf_VFX);
    }

}
