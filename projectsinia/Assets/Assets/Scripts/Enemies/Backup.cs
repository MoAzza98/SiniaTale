
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Health : MonoBehaviour
// {

//     [SerializeField] int health = 50;
//     [SerializeField] bool isPlayer;
//     [SerializeField] float invincibilityTime = 5f;

//     bool hasTakenDamage = false;
//     bool isInvincible = false;
//     CapsuleCollider2D myBodyCollider;
//     Rigidbody2D myRigidBody;

//     #region Knockback
//     [SerializeField] float knockbackForce = 5f;
//     #endregion

//     void Awake()
//     {
//         myBodyCollider = GetComponent<CapsuleCollider2D>();
//         myRigidBody = GetComponent<Rigidbody2D>();
//     }

//     private void Update()
//     {
//         // if(hasTakenDamage == true){

//         // }
//     }

//     IEnumerator resetInvincibility()
//     {
//         Debug.Log("well currently the player should have been hit, and invincible: " + isInvincible);
//         yield return new WaitForSeconds(invincibilityTime);
//         Debug.Log("now setting isinvincible to false");
//         isInvincible = false;

//     }

//     public int GetCurrentHealth()
//     {
//         return health;
//     }

//     void OnCollisionEnter2D(Collision2D other)
//     {

//         if (other.gameObject.tag == "Enemy" && !isInvincible)
//         {
//             Debug.Log("taking dam, isinvinvible should be false..." + isInvincible);
//             DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
//             // Calculate the direction of the knockback
//             Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
//             Debug.Log("Logging knockbackDirection... " + knockbackDirection);
//             Debug.Log(knockbackDirection);
//             float xSign = Mathf.Sign(knockbackDirection.x);
        
//             // Apply the knockback force to the player's rigidbody
//             myRigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
//             // Debug.Log("Now logging the new rb velocity: " + new Vector2(xSign * knockbackForce, knockbackForce));
//             // GetComponent<Rigidbody2D>().velocity = new Vector2(xSign * knockbackForce, knockbackForce);
//             if (damageDealer)
//             {
//                 TakeDamage(damageDealer.GetDamage());
//             }

//             isInvincible = true;

//             StartCoroutine(resetInvincibility());
//         }


//     }


//     void TakeDamage(int damage)
//     {
//         health -= damage;
//         Debug.Log("new health is: " + health);

//         if (health <= 0 && isPlayer)
//         {
//             Debug.Log("You died! ");
//         }
//         else if (health <= 0 && !isPlayer)
//         {
//             Destroy(gameObject);
//         }


//     }

//     void PlayHitEffect()
//     {
//         //potentially play take damage animation.
//     }

// }