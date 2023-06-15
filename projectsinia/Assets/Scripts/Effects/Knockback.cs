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

    public void PlayFeedback(GameObject applier, float customKnockbackX = -1f, float customKnockbackY = -1f)
    {

        Vector2 direction = (transform.position - applier.transform.position).normalized;
        if (customKnockbackX == -1 && customKnockbackY == -1)
        {
            myRigidBody.AddForce(new Vector2(direction.x * knockbackDistanceX, direction.y * knockbackDistanceY), ForceMode2D.Impulse);
        }
        else
        {
            myRigidBody.AddForce(new Vector2(direction.x * customKnockbackX, direction.y * customKnockbackY), ForceMode2D.Impulse);

        }


    }

}
