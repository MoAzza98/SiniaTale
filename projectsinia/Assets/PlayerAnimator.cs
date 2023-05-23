using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerMovement mov;
    private SpriteRenderer sr;
    public Animator anim;

    public bool justLanded { private get; set; }
    public bool justJumped { private get; set; }
    public bool isMidair { private get; set; }
    private float currentY;

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<PlayerMovement>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateAnimationState();
        //Debug.Log(mov.LastOnGroundTime);
    }

    void UpdateAnimationState()
    {
        anim.SetFloat("Vel X", Mathf.Abs(mov.RB.velocity.x));
        currentY = mov.RB.velocity.y;
        anim.SetFloat("Vel Y", currentY);
        
        if (justLanded)
        {
            anim.SetTrigger("JustLanded");
            justLanded = false;
        }
        
        if (justJumped)
        {
            anim.SetTrigger("JustJumped");
            justJumped = false;
        }
        if (mov.RB.velocity.y < -0.1f)
        {
            anim.SetBool("Falling", true);
        } else
        {
            anim.SetBool("Falling", false);
        }
    }
}
