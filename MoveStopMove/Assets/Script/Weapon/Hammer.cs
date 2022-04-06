using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GameUnit
{
    public Rigidbody rigidbody;

    //public ParticleSystem hitVFX;

    public void OnInit()
    {
        rigidbody.velocity = Transform.forward * 20f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Debug.Log("Hit");
            //TODO: fix late
            //Destroy(gameObject);

            //ParticlePool.Play(hitVFX, Transform.position, Quaternion.identity);

            HammerPool.Despawn(this);
        }

    }

}
