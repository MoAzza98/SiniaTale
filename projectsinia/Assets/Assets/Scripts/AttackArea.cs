using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    //We need to clean up these variables...
    //TODO: Take a lot of these values and put them into scriptable objects, damage, crit rate, etc etc.
    //Handle more logic in playerattack script

    public int damage = 3;

    Health otherHealth;

    Knockback knockback;

    GameObject myCopy;

    int counter = 0;

    [SerializeField] float attackDuration = 0.01f;
    [SerializeField] private bool isSpecial;

    private bool isCrit;
    bool isInvincible = false;
    EnemyMovement otherMovement;
    PlayerMovement playerMovement;
    private Transform enemyTransform;

    [SerializeField] private int numberOfHits;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject critHitEffect;

    private ParticleSystem slashEffect;
    Canvas canvas;
    TextMeshProUGUI damageText;
    private int hitDamage;

    private GameObject obj;
    private GameObject hitVFX;
    private GameObject critVFX;

    private void Awake()
    {
        slashEffect = attackEffect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule slashEffectSettings = slashEffect.main;
        obj = Instantiate(attackEffect, transform.position, Quaternion.Euler(0, 0, gameObject.transform.parent.transform.position.z));
        obj.SetActive(false);
        hitDamage = damage;
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        if (isSpecial)
        {
            obj.SetActive(true);
            obj.transform.rotation = gameObject.transform.parent.rotation;
            obj.transform.parent = gameObject.transform;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>() && !isInvincible)
        {
            enemyTransform = other.GetComponentInChildren<Transform>();
            knockback = other.GetComponent<Knockback>();
            otherHealth = other.GetComponent<Health>();
            otherMovement = other.GetComponent<EnemyMovement>();
            if(other.tag == "Enemy"){
                otherMovement.PauseEnemyMovement();
            }
            
            // Debug.Log("logging enemy otherhealth BEFORE : " + otherHealth.GetCurrentHealth());
            if (otherHealth.GetCurrentHealth() > 0)
            {
                for(int i = 0; i <= numberOfHits - 1; i++)
                {
                    hitDamage = damage;
                    if (Random.Range(1, 100) > 70f)
                    {
                        isCrit = true;
                        hitDamage = hitDamage * 2;
                        if (isSpecial)
                        {
                            critVFX = Instantiate(critHitEffect, transform.position, Quaternion.identity);
                        }
                    } else
                    {
                        isCrit = false;
                        if (isSpecial)
                        {
                            hitVFX = Instantiate(hitEffect, transform.position, Quaternion.identity);
                        }
                    }
                    otherHealth.TakeDamage(hitDamage);
                    if (other.tag == "Enemy")
                    {
                        CinemachineShake.Instance.ShakeCamera(4f, 0.2f);
                        DamagePopup.Create(enemyTransform.position, hitDamage, isCrit);
                    }

                    knockback.PlayFeedback(gameObject.transform.parent.gameObject);
                }

                // Vector2 spawnPosition = Camera.main.WorldToScreenPoint(other.transform.position);
                // Debug.Log("Took dam, logging spawnpos : " + spawnPosition);
                
                // TextMeshProUGUI tmpText = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.identity).GetComponentInChildren<TextMeshProUGUI>();
                // tmpText.text = damage.ToString();
                // Debug.Log("Logging gameobject pos: " + tmpText.gameObject.transform.position);
                /*
                RectTransform textTransform = Instantiate(damageNumberPrefab).GetComponentInChildren<DamageNumber>().GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
                damageText = textTransform.gameObject.GetComponent<TextMeshProUGUI>();
                damageText.text = damage.ToString();

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
                */
                

                // Debug.Log("logging enemy otherhealth AFTER : " + otherHealth.GetCurrentHealth());

            }

            isInvincible = true;

            // Start a coroutine to reset the flag after a delay
            StartCoroutine(ResetInvincibility());
        }
        else if (isInvincible)
        {
            StartCoroutine(ResetInvincibility());
        }

    }

    private void OnDisable()
    {
        obj.SetActive(false);
    }

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(attackDuration);
        isInvincible = false;
    }

}
