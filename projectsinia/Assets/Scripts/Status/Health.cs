using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 100;

    [SerializeField] bool isPlayer;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject thanksPanel;

    #region take damage and knockback
    Rigidbody2D myRigidBody;

    bool isInvincible = false;

    [SerializeField] float invincibilityTime = 0f;

    Knockback knockback;

    [SerializeField] float touchEnemyKnockbackX;
    [SerializeField] float touchEnemyKnockbackY;
    #endregion

    #region Healthbar
    private GameObject healthbarObject;
    [SerializeField] GameObject healthBarPrefab;
    HPBar myHealthBar;
    #endregion

    [SerializeField] GameObject damageNumberPrefab;
    private GameViewManager GManager;

    [SerializeField] float despawnTime = 3f;
    Animator myAnimator;
    [SerializeField] float gameOverTime = 3f;

    public int GetCurrentHealth()
    {
        return health;
    }


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (isPlayer)
        {
            GManager = GameObject.Find("GameManager").GetComponent<GameViewManager>();
        }

        myHealthBar = GetComponentInChildren<HPBar>();
        myHealthBar.SetMaxHealth((float)health);


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


            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer)
            {
               
                TakeDamage(damageDealer.GetDamage());
                knockback.PlayFeedback(other.gameObject, touchEnemyKnockbackX, touchEnemyKnockbackY);

            }
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

        //consider moving to attackarea to have diff types of damage popups.
        // Vector3 spawnPosition = gameObject.transform.position;

        // spawnPosition = new Vector3(-36.5f, -7.4f, 0f);
        // Debug.Log("Took dam, logging spawnpos : " + spawnPosition);
        // TextMeshProUGUI tmpText = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.identity).GetComponentInChildren<TextMeshProUGUI>();
        // tmpText.text = damage.ToString();

        if (health <= 0 && isPlayer)
        {

            Debug.Log("You died");
            GetComponent<PlayerAnimator>().KillPlayer();
            Invoke("GameOver", gameOverTime);
            // GManager.GameOver();
            myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            gameObject.layer = LayerMask.NameToLayer("Dead");
            gameObject.tag = "Dead";
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<PlayerAttack>().AttackEnded();
            GetComponentInChildren<PlayerAttack>().enabled = false;
            GetComponentInChildren<AttackArea>().enabled = false;
            // myAnimator.SetBool("Attacking", false);
            // if(GetComponentInChildren<AttackArea>().enabled){
            //     GetComponentInChildren<AttackArea>().enabled = false;
            // }







            // GameOver();



        }
        else if (health <= 0 && !isPlayer)
        {
            if (isBoss)
            {
                thanksPanel.SetActive(true);
            }
            myAnimator.SetTrigger("Die");
            myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<EnemyMovement>().killEnemy();

            Destroy(gameObject, despawnTime);
        }


    }
    #endregion
    void GameOver()
    {
        GManager.GameOver();
    }
}
