using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    public int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        /*
        if(other.GetComponent<Health>() != null)
        {
            //apply damage here
        }
        */
    }
}
