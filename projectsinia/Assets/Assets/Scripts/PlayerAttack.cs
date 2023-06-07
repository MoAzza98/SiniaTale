using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject specialAttackArea;
    public GameObject specialAttackPivot;

    private GameObject specialAttack;
    private GameObject attackArea;
    private Animator anim;
    private GameObject player;
    
    public bool attacking = false;
    public bool specialAttacking = false;

    private float timeToAttack = 0.1f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.gameObject;
        attackArea = player.GetComponentInChildren<AttackArea>().gameObject;
        specialAttack = Instantiate(specialAttackArea, new Vector3(specialAttackPivot.transform.position.x + 4f, specialAttackPivot.transform.position.y), transform.rotation);
        specialAttack.transform.parent = specialAttackPivot.transform;
        attackArea.SetActive(attacking);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attacking = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpecialAttack();
        }
        if (specialAttacking)
        {
            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                SpecialAttackEnded();
                timer = 0f;
            }
        }
        /*
        if (attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                AttackEnded();
            }
        }
        */
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
    }
}
