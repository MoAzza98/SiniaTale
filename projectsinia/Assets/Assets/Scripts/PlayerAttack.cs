using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea;
    private Animator anim;
    private GameObject player;

    public bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.gameObject;
        attackArea = player.GetComponentInChildren<AttackArea>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attacking = true;
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
