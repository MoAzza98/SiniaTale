using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //Movement settings
    [SerializeField] float moveSpeed = 1f;
    float moveSpeed2;
    Rigidbody2D enemyRigidBody;
    bool isMoving = true;
    [SerializeField] bool shouldPatrol = true;
    [SerializeField] float moveTimeVariance = 10f;
    [SerializeField] float idleTimeVariance = 2f;
    [SerializeField] float chanceToFlipDirection = 0.25f;
    float moveTime;
    float idleTime;
    bool isPatrolling = false;

    //Animation
    Animator enemyAnimator;

    //AI Chase
    Transform playerTransform;
    bool isChasing = false;
    [SerializeField] float chaseDistance = 3f;
    [SerializeField] float dropChaseMultiplier = 2f;

    //Misc
    bool firstPatrolCall = true;


    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerAttack>().GetComponent<Transform>();
        moveSpeed2 = moveSpeed;

    }

    void Update()
    {
        if(enemyRigidBody.velocity.x != 0){
            transform.localScale = new Vector2((Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
        }
        
        if (isChasing)
        {
            isPatrolling = false;
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance * dropChaseMultiplier)
            {
                Debug.Log("Should stop chasing");
                isChasing = false;
            }
            else
            {
                ChasePlayer();
            }

        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                Debug.Log("Setting is chasing to be true");
                isChasing = true;
            }
            else
            {
                isChasing = false;
                if (!isPatrolling)
                {
                    shouldPatrol = true;
                    isPatrolling = true;
                    Debug.Log("Stopped chasing, should call patrol once.");
                    if(!firstPatrolCall)
                    {
                        Debug.Log("Should only show on 2nd or more calls. Anyway flipping broy");
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                  
                    }
                    firstPatrolCall = false;
                    Patrol();
                }
            }


        }
    }

    void ChasePlayer()
    {
        // Debug.Log("Should be chasing... movespeed is: " + moveSpeed);
        enemyAnimator.SetBool("IsMoving", true);
        if (transform.position.x > playerTransform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.left * moveSpeed2 * Time.deltaTime;
        }
        if (transform.position.x < playerTransform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.right * moveSpeed2 * Time.deltaTime;
        }

    }



    void Patrol()
    {
        StartCoroutine(MoveForSeconds());
    }
    IEnumerator MoveForSeconds()
    {

        while (shouldPatrol)
        {
            //get random move and idle time.
            moveTime = Random.Range(0f, moveTimeVariance);
            idleTime = Random.Range(0f, idleTimeVariance);

            //begin moving
            float elapsedTime = 0f;
            enemyAnimator.SetBool("IsMoving", true);


            if (Random.Range(0f, 1f) <= chanceToFlipDirection)
            {
                moveSpeed = -moveSpeed;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                enemyRigidBody.velocity = new Vector2(0f, 0f);

            }

            while (elapsedTime < moveTime)
            {
                enemyRigidBody.velocity = new Vector2(moveSpeed, 0f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }



            //stop moving, begin idling
            enemyAnimator.SetBool("IsMoving", false);
            float elapsedIdleTime = 0f;
            while (elapsedIdleTime < idleTime)
            {
                enemyRigidBody.velocity = Vector2.zero;
                elapsedIdleTime += Time.deltaTime;
                yield return null;
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
        enemyRigidBody.velocity = new Vector2(0f, 0f);
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }


}


