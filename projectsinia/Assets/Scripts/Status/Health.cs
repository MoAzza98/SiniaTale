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
    private GameObject healthBarPrefab;
    private HPBar myHealthBar;
    #endregion

    [SerializeField] GameObject damageNumberPrefab;
    [SerializeField] private GameViewManager GManager;

    [SerializeField] float despawnTime = 3f;
    Animator myAnimator;
    [SerializeField] float gameOverTime = 3f;

    private SpriteRenderer playerSpriteRenderer;
    [SerializeField] float onDamageFlashRedDuration = 0.3f;
    [SerializeField] float onDamageEnemyFlashDuration = 0.2f;

    public int GetCurrentHealth()
    {
        return health;
    }


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        myAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        /*
        if (isPlayer)
        {
            healthBarPrefab = GameObject.Find("PlayerHealthBar");
        }
        */
    }

    private void Start()
    {
        if (isPlayer)
        {
            GManager = GameObject.Find("GameManager").GetComponent<GameViewManager>();
            healthBarPrefab = GameObject.Find("PlayerHealthBar");
            myHealthBar = healthBarPrefab.GetComponentInChildren<HPBar>();
        }
        else
        {
            myHealthBar = GetComponentInChildren<HPBar>();
        }

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
        
        myHealthBar.SetCurrentHealth(-((float)damage));
        health -= damage;
        
        if(isPlayer){
            StartCoroutine(FlashRed());
        }else{
            StartCoroutine(EnemyTakeDamageFlash());
        }

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

    IEnumerator FlashRed(){
        playerSpriteRenderer.color = ColorUtility.TryParseHtmlString("#D22D2D", out Color color) ? color : Color.white;
        yield return new WaitForSeconds(onDamageFlashRedDuration);
        playerSpriteRenderer.color = Color.white;

    }

    IEnumerator EnemyTakeDamageFlash(){
        playerSpriteRenderer.color = ColorUtility.TryParseHtmlString("#EC4747", out Color color) ? color : Color.white;
        yield return new WaitForSeconds(onDamageEnemyFlashDuration);
        playerSpriteRenderer.color = Color.white;

    }
}
