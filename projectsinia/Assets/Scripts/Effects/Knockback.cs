using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float knockbackStrength = 16f;
    [SerializeField] float knockbackDuration = 0.15f;
    [SerializeField] float knockbackDistanceX = 5f;
    [SerializeField] float knockbackDistanceY = 5f;


    public UnityEvent OnBegin, OnDone;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        myRigidBody.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

    public void PlayFeedback(GameObject applier)
    {
        // Debug.Log("Yes we are playing knockback..." + applier);
        StopAllCoroutines();
        OnBegin?.Invoke();
        // myRigidBody.velocity = Vector3.zero;
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        myRigidBody.AddForce(new Vector2(direction.x * knockbackDistanceX, direction.y * knockbackDistanceY), ForceMode2D.Impulse);
        // StartCoroutine(ResetKnockback());
    }

}
