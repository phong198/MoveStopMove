using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    public Character _owner;
    public float bulletSpeed;
    public int damageType;
    public int bulletDamage;
    public float despawnTime = 1;
    public float despawnTimer;

    public void OnInit(Character owner)
    {
        _owner = owner;
        despawnTimer = despawnTime;
    }

    public void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;

        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
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
                damage.Damage(damageType, bulletDamage, _owner.characterDamage, _owner);
            }
            //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);
            PoolSystem.Despawn(this);
            _owner.Hit();
        }
    }
}
