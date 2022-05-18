using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public int boostID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            IBoost boost = other.GetComponent<IBoost>();
            if (boost != null)
            {
                boost.Boost(boostID);
            }
            //Destroy(gameObject);
        }
    }
}
