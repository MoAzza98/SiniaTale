using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidBody;
    [SerializeField] float knockbackStrength = 16f;
    [SerializeField] float knockbackDuration = 2f;
    [SerializeField] float knockbackDistanceX = 200f;
    [SerializeField] float knockbackDistanceY = 50f;


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
        // StopAllCoroutines();
        // OnBegin?.Invoke();
        Vector2 direction = (transform.position - applier.transform.position).normalized;
        // direction += (myRigidBody.velocity).normalized;
        // myRigidBody.AddForce(new Vector2(-(direction.x * knockbackDistanceX), 0f), ForceMode2D.Impulse);
        myRigidBody.AddForce(new Vector2(direction.x * knockbackDistanceX, direction.y * knockbackDistanceY), ForceMode2D.Impulse);
        // StartCoroutine(ResetKnockback());
    }

}
