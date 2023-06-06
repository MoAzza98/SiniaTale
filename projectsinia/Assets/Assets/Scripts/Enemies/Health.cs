using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 50;

    [SerializeField] bool isPlayer;

    Rigidbody2D myRigidBody;

    bool isInvincible = false;

    [SerializeField] float invincibilityTime = 5f;

    public int GetCurrentHealth()
    {
        return health;
    }

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }



    void OnTriggerStay2D(Collider2D other)
    {
        if (isPlayer && other.tag == "Enemy" && !isInvincible)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer)
            {
                TakeDamage(damageDealer.GetDamage());

            }
            // Debug.Log("Setting invincibility to true");
            isInvincible = true;

            // Start a coroutine to reset the flag after a delay
            StartCoroutine(ResetInvincibility());
        }
    }


    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(invincibilityTime);
        // Debug.Log("Now setting invincibility to false.");
        isInvincible = false;
    }

    void TakeDamage(int damage)
    {

        health -= damage;
        Debug.Log("Took Damage, your new health is: " + health);
        if (health <= 0 && isPlayer)
        {
            Debug.Log("You died");
        }
        else if (health <= 0 && !isPlayer)
        {
            Destroy(gameObject);
        }


    }

}
