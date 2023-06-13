using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
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

    [SerializeField] GameObject damageNumberPrefab;

    Canvas canvas;
    TextMeshProUGUI damageText;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }


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

                // Vector2 spawnPosition = Camera.main.WorldToScreenPoint(other.transform.position);
                // Debug.Log("Took dam, logging spawnpos : " + spawnPosition);
                
                // TextMeshProUGUI tmpText = Instantiate(damageNumberPrefab, spawnPosition, Quaternion.identity).GetComponentInChildren<TextMeshProUGUI>();
                // tmpText.text = damage.ToString();
                // Debug.Log("Logging gameobject pos: " + tmpText.gameObject.transform.position);

                RectTransform textTransform = Instantiate(damageNumberPrefab).GetComponentInChildren<DamageNumber>().GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
                damageText = textTransform.gameObject.GetComponent<TextMeshProUGUI>();
                damageText.text = damage.ToString();

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);

                

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

    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(attackDuration);
        isInvincible = false;
    }

}
