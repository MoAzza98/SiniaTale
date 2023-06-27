using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector] public PlayerMovement mov;
    private SpriteRenderer sr;
    [HideInInspector] public Animator anim;
    public PlayerAttack pAttack;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    private ParticleSystem _jumpParticle;
    private ParticleSystem _landParticle;

    public bool justLanded { private get; set; }
    public bool justJumped { private get; set; }
    public bool isMidair { private get; set; }
    private float currentY;

    private bool isDead = false;
    private bool deathAnimPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        pAttack = GetComponentInChildren<PlayerAttack>();
        mov = GetComponent<PlayerMovement>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
        _landParticle = landFX.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateAnimationState();

        ParticleSystem.MainModule jumpPSettings = _jumpParticle.main;
        jumpPSettings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
        ParticleSystem.MainModule landPSettings = _landParticle.main;
        landPSettings.startColor = new ParticleSystem.MinMaxGradient(Color.white);
    }

    void UpdateAnimationState()
    {
        if (!deathAnimPlayed)
        {
            anim.SetFloat("Vel X", Mathf.Abs(mov.RB.velocity.x));
            currentY = mov.RB.velocity.y;
            anim.SetFloat("Vel Y", currentY);

            if (justLanded)
            {
                anim.SetTrigger("JustLanded");
                GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
                Destroy(obj, 1);
                justLanded = false;
            }

            if (justJumped)
            {
                anim.SetTrigger("JustJumped");
                GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
                Destroy(obj, 1);
                justJumped = false;
            }
            if (mov.RB.velocity.y < -0.01f && mov.LastOnGroundTime < 0.1f)
            {
                anim.SetBool("Falling", true);
            }
            else
            {
                anim.SetBool("Falling", false);
            }

            if (pAttack.attacking || pAttack.specialAttacking)
            {
                anim.SetBool("Attacking", true);
            }
            else if (!pAttack.attacking && !pAttack.specialAttacking)
            {
                anim.SetBool("Attacking", false);
            }


            if (isDead)
            {
                anim.SetTrigger("Die");
                isDead = false;
                deathAnimPlayed = true;
            }
        }else{

            // anim.SetTrigger("Die");
            anim.SetBool("Attacking", false);
        }




    }

    public void KillPlayer()
    {
        Debug.Log("in killplayer... setting isdead to true");
        isDead = true;
    }
}
