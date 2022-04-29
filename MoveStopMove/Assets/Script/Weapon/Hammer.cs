using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GameUnit
{
    public Rigidbody _rigidbody;

    //public ParticleSystem hitVFX;

    public void OnInit()
    {
        _rigidbody.velocity = Transform.forward * 10f;
    }

    //public void Update()
    //{
    //    float travelRange = (Character.transform.position - gameObject.transform.position).magnitude;
    //    if(travelRange > Character.rad)
    //    {
    //        HammerPool.Despawn(this);
    //    }    
    //}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);
            HammerPool.Despawn(this);
            //Character.Hit();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        HammerPool.Despawn(this);
    }

}
