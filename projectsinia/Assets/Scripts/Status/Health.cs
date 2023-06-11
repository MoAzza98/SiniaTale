using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 100;

    [SerializeField] bool isPlayer;

    #region take damage and knockback
    Rigidbody2D myRigidBody;

    bool isInvincible = false;

    [SerializeField] float invincibilityTime = 0f;

    Knockback knockback;
    #endregion

    #region Healthbar
    private GameObject healthbarObject;
    [SerializeField] GameObject healthBarPrefab;
    HPBar myHealthBar;
    #endregion

    public int GetCurrentHealth()
    {
        return health;
    }


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        if (!isPlayer)
        {
            myHealthBar = GetComponentInChildren<HPBar>();
            myHealthBar.SetMaxHealth((float)health);
        }

    }




    #region Taking damage & knockback
    void OnTriggerStay2D(Collider2D other)
    {
        if (isPlayer && other.tag == "Enemy" && !isInvincible)
        {
            knockback.PlayFeedback(other.gameObject);
            isInvincible = true;
            StartCoroutine(ResetInvincibility());
            // TakeDamage(damageDealer.GetDamage());

            
            // DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            // if (damageDealer)
            // {
            //     TakeDamage(damageDealer.GetDamage());
            //     knockback.PlayFeedback(other.gameObject);

            // }
            

            // Start a coroutine to reset the flag after a delay
            
        }
        //else if (!isPlayer && other.tag == "Player" && myHealthBar)
        //{
        // Debug.Log("Yea hit registered.");
        // knockback.PlayFeedback(other.gameObject);
        // DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        // if (damageDealer)
        // {
        //     TakeDamage(damageDealer.GetDamage());
        //     // Debug.Log("Logging current health after taking damage: " + health);
        //     myHealthBar.SetCurrentHealth(-((float)damageDealer.GetDamage()));
        // }
        //}
        // else if(!isPlayer && other.tag == "Player"){ //&& other.gameObject.GetComponent<AttackArea>() && other.tag == "Player"){
        //     Debug.Log("about to knockback enemy");
        //     knockback.PlayFeedback(other.gameObject);
        // }
    }


    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(invincibilityTime);
        // Debug.Log("Now setting invincibility to false.");
        isInvincible = false;
    }

    public void TakeDamage(int damage)
    {
        // Debug.Log("Taking damage, value is : " + damage);
        myHealthBar.SetCurrentHealth(-((float)damage));
        health -= damage;
        // Debug.Log("Took Damage, your new health is: " + health);
        if (health <= 0 && isPlayer)
        {
            Debug.Log("You died");
        }
        else if (health <= 0 && !isPlayer)
        {
            Destroy(gameObject);
        }


    }
    #endregion

}
