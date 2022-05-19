using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public Character _owner;
    public float bulletSpeed;
    public int bulletID;

    public void OnInit(Character owner)
    {
        _owner = owner;
    }

    public void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        float travelRange = (_owner.transform.position - gameObject.transform.position).magnitude;
        if (travelRange >= _owner.attackRadius)
        {
            PoolSystem.Despawn(this);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            IDamage damage = other.transform.GetComponent<IDamage>();
            if (damage != null)
            {
                damage.Damage(bulletID, _owner.characterDamage, _owner);
            }
            //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);
            PoolSystem.Despawn(this);
            _owner.Hit();
        }
    }
}
