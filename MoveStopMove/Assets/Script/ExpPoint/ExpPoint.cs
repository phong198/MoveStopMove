using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : GameUnit
{
    public int expID;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            if(character != null)
            {
                character.IncreaseXP(expID, 0);
            }
            PoolSystem.Despawn(this);
            DecreaseCount();
        }
    }
    public virtual void DecreaseCount() { }
}
