using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{

    //move all of this into a scriptable object later
    public GameObject specialAttackArea;
    public GameObject specialAttackPivot;

    private GameObject specialAttack;
    private GameObject attackArea;
    private Animator anim;
    private GameObject player;
    private GameViewManager GM;

    private float currentCooldown = 0f;
    private bool energyDepleted = false;
    private float timeToAttack = 0.1f;
    private float timer = 0f;
    
    [HideInInspector] public bool attacking = false;
    [HideInInspector] public bool specialAttacking = false;


    [SerializeField] private float energy = 100f;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energyCost = 10f;
    [SerializeField] private float rechargeRate = 30f;
    [SerializeField] private float rechargeCooldown = 3f;
    [SerializeField] private TextMeshProUGUI energyNum;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameViewManager>();
        anim = GetComponent<Animator>();
        player = transform.parent.gameObject;
        attackArea = player.GetComponentInChildren<AttackArea>().gameObject;
        specialAttack = Instantiate(specialAttackArea, new Vector3(specialAttackPivot.transform.position.x + 4f, specialAttackPivot.transform.position.y), transform.rotation);
        specialAttack.transform.parent = specialAttackPivot.transform;
        attackArea.SetActive(attacking);
        GM.energyScore = energy;
    }

    // Update is called once per frame
    void Update()
    {

        //Attacking is set to true, used in animationevent to activate the hitbox when the animation starts
        if (Input.GetKeyDown(KeyCode.F))
        {
            attacking = true;
        }

        if (!energyDepleted)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if(energy > 100)
                {
                    energy = 100;
                }
                currentCooldown = 0f;
                SpecialAttack();
                if (energy <= maxEnergy && energy > 0)
                {
                    energy -= energyCost * Time.deltaTime;
                }
                GM.energyScore = energy;
                // Debug.Log(GM.energyScore);
                if (energy < 0)
                {
                    energyDepleted = true;
                }
            }
            else
            {
                if(currentCooldown >= rechargeCooldown)
                {
                    if(energy < maxEnergy && energy > 0)
                    {
                        energy += rechargeRate * Time.deltaTime;
                    }
                    GM.energyScore = energy;
                    energyNum.SetText(Mathf.RoundToInt(energy).ToString());
                }
                SpecialAttackEnded();
            }
        } else
        {
            SpecialAttackEnded();
            energy += rechargeRate * Time.deltaTime;
            GM.energyScore = energy;
            if (energy >= 100)
            {
                energy = 100;
                energyDepleted = false;
            }
        }

        Debug.Log("slider value: " + GM.energyScore + " energy value: " + energy + "energy depleted is: " + energyDepleted);
        currentCooldown += Time.deltaTime;

        /*
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
