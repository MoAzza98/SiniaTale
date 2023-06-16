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
    [SerializeField] float attackRadius = 0.25f;
    float moveTime;
    float idleTime;
    bool isPatrolling = false;

    //Animation
    Animator enemyAnimator;

    //AI Chase
    public Transform playerTransform;
    bool isChasing = false;
    [SerializeField] float chaseDistance = 3f;
    [SerializeField] float dropChaseMultiplier = 2f;
    [SerializeField] float chaseSpeed = 5f;
    [SerializeField] float attackDistance = 10f;


    //Misc
    bool firstPatrolCall = true;
    bool pauseMovement = false;
    [SerializeField] float knockbackInvincibilityDuration = 0.1f;

    [SerializeField] public LayerMask _playerLayer;



    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerAttack>().GetComponent<Transform>();
        moveSpeed2 = moveSpeed;

    }


    void Update()
    {
     

        if (!pauseMovement)
        {
            //check if player near.
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                // Debug.Log("Setting is chasing to be true");
                if (enemyRigidBody.velocity.x != 0)
                {
                    transform.localScale = new Vector2((Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
                }
                isChasing = true;
                shouldPatrol = false;
                StopAllCoroutines();
                ChasePlayer();
            }
            else
            {



                if (enemyRigidBody.velocity.x != 0)
                {
                    transform.localScale = new Vector2((Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
                }

                if (isChasing)
                {
                    isPatrolling = false;
                    shouldPatrol = false;
                    if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance * dropChaseMultiplier)
                    {
                        // Debug.Log("Should stop chasing");
                        isChasing = false;
                        shouldPatrol = true;
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
                        // Debug.Log("Setting is chasing to be true");
                        isChasing = true;
                    }
                    else
                    {
                        isChasing = false;
                        if (!isPatrolling)
                        {
                            shouldPatrol = true;
                            isPatrolling = true;
                            // Debug.Log("Stopped chasing, should call patrol once.");
                            if (!firstPatrolCall)
                            {
                                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                            }
                            firstPatrolCall = false;
                            Patrol();
                        }
                    }


                }
            }
        }
        else
        {
            StartCoroutine(RestartMovement());
        }


    }

    void ChasePlayer()
    {
        // Debug.Log("Should be chasing... movespeed is: " + chaseSpeed);

        if (enemyAnimator.GetBool("Attacking"))
        {
            
            enemyAnimator.SetBool("IsMoving", false);
            if (transform.position.x > playerTransform.position.x)
            {
                enemyRigidBody.velocity = new Vector2(-Mathf.Abs(0.01f), 0f);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                enemyRigidBody.velocity = new Vector2(Mathf.Abs(0.01f), 0f);
            }
            enemyRigidBody.velocity = Vector2.zero;
        }
        else
        {
            enemyAnimator.SetBool("IsMoving", true);
            if (transform.position.x > playerTransform.position.x)
            {
                // transform.localScale = new Vector3(-1, 1, 1);
                // transform.position += Vector3.left * moveSpeed2 * Time.deltaTime;

                //can try addforce impulse to make it faster.
                enemyRigidBody.velocity = new Vector2(-Mathf.Abs(chaseSpeed), 0f);
            }
            if (transform.position.x < playerTransform.position.x)
            {
                // transform.localScale = new Vector3(1, 1, 1);
                // transform.position += Vector3.right * moveSpeed2 * Time.deltaTime;
                // moveSpeed = -moveSpeed;
                enemyRigidBody.velocity = new Vector2(Mathf.Abs(chaseSpeed), 0f);
            }
        }


    }

    IEnumerator RestartMovement()
    {
        yield return new WaitForSeconds(knockbackInvincibilityDuration);
        pauseMovement = false;
    }

    public void PauseEnemyMovement()
    {
        pauseMovement = true;
    }



    void Patrol()
    {
        StartCoroutine(MoveForSeconds());
    }
    IEnumerator MoveForSeconds()
    {

        while (shouldPatrol)
        {
            //check if player near.
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                // Debug.Log("Setting is chasing to be true");
                isChasing = true;
                shouldPatrol = false;
                StopAllCoroutines();
                ChasePlayer();
            }
            else
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
    }


    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        // FlipEnemyFacing();
        enemyRigidBody.velocity = new Vector2(0f, 0f);
    }

    void FlipEnemyFacing()
    {

        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }


}


