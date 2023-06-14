using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject specialAttackArea;
    public GameObject specialAttackPivot;
    public float attackRadius;
    [SerializeField] public LayerMask _playerLayer;

    private GameObject specialAttack;
    private GameObject attackArea;
    private Animator anim;
    private GameObject enemy;

    [HideInInspector] public bool attacking = false;
    [HideInInspector] public bool specialAttacking = false;

    private float timeToAttack = 1.5f;
    private float timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = transform.gameObject;
        attackArea = enemy.GetComponentInChildren<AttackArea>().gameObject;
        anim = GetComponent<Animator>();
        //specialAttack = Instantiate(specialAttackArea, new Vector3(specialAttackPivot.transform.position.x + 4f, specialAttackPivot.transform.position.y), transform.rotation);
        //specialAttack.transform.parent = specialAttackPivot.transform;
        attackArea.SetActive(attacking);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("Timer:" + timer + "Overlap: " + Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 2), attackRadius, _playerLayer));
        //Attacking is set to true, used in animationevent to activate the hitbox when the animation starts
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 2), attackRadius, _playerLayer))
        {
            if(timer >= timeToAttack)
            {
                anim.SetBool("Attacking", true);
                timer = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 2), attackRadius);
    }

    public void SpecialAttack()
    {
        specialAttacking = true;
        specialAttack.SetActive(specialAttacking);
    }

    public void SpecialAttackEnded()
    {
        specialAttacking = false;
        specialAttack.SetActive(specialAttacking);
    }

    public void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }

    public void AttackEnded()
    {
        attacking = false;
        attackArea.SetActive(attacking);
        anim.SetBool("Attacking", false);
    }
}
