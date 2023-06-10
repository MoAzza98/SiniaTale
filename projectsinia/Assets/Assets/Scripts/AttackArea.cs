using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    public int damage = 3;

    Health otherHealth;

    Knockback knockback;

    GameObject myCopy;

    int counter = 0;


    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("How many times does this play per hit? : " + counter);
        counter++;
        
        knockback = other.GetComponent<Knockback>();
        // Debug.Log("Attack area ontrigger triggered." + other);
        otherHealth = other.GetComponent<Health>();
        Debug.Log("logging enemy otherhealth BEFORE" + otherHealth.GetCurrentHealth());
        if (otherHealth.GetCurrentHealth() > 0)
        {
            otherHealth.TakeDamage(damage); 
            // Debug.Log("about to knockback enemy");

            // myCopy = this.gameObject;
            // Debug.Log("printing gameobjects transform: " + myCopy.transform);
            // knockback.PlayFeedback(myCopy);

            Debug.Log("logging enemy otherhealth AFTER : " + otherHealth.GetCurrentHealth());

        }
    }

}
