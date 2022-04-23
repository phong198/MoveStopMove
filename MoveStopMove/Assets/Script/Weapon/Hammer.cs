using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GameUnit
{
    public Rigidbody rigidbody;

    //public ParticleSystem hitVFX;

    public void OnInit()
    {
        rigidbody.velocity = Transform.forward * 10f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);
            HammerPool.Despawn(this);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        HammerPool.Despawn(this);
    }

}
