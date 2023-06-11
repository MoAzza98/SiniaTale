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

    [SerializeField] float attackDuration = 0.01f;
    bool isInvincible = false;
    EnemyMovement otherMovement;
    PlayerMovement playerMovement;




    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Is this trigger even entering? : " + other);
        // Debug.Log("Logging enemy and isinvincible: " + other.tag + isInvincible);
        if (other.tag == "Enemy" && !isInvincible)
        {
            knockback = other.GetComponent<Knockback>();
            otherHealth = other.GetComponent<Health>();
            otherMovement = other.GetComponent<EnemyMovement>();
            otherMovement.PauseEnemyMovement();
            // Debug.Log("logging enemy otherhealth BEFORE : " + otherHealth.GetCurrentHealth());
            if (otherHealth.GetCurrentHealth() > 0)
            {
                otherHealth.TakeDamage(damage);

                knockback.PlayFeedback(gameObject);

                // Debug.Log("logging enemy otherhealth AFTER : " + otherHealth.GetCurrentHealth());

            }

            isInvincible = true;

            // Start a coroutine to reset the flag after a delay
            StartCoroutine(ResetInvincibility());
        }else if(isInvincible){
            StartCoroutine(ResetInvincibility());
        }

    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(attackDuration);
        isInvincible = false;
    }

}
