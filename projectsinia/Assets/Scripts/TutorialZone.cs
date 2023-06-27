using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public enum Section // your custom enumeration
    {
        Dash,
        Move,
        Rewind,
        WallJump,
        Attack
    };

    public Section section;
    private TutorialGuide tutGuide;

    // Start is called before the first frame update
    void Start()
    {
        tutGuide = gameObject.GetComponentInParent<TutorialGuide>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (section)
        {
            case Section.Dash:
                tutGuide.DashInfo();
                break;

            case Section.Move:
                tutGuide.MoveInfo();
                break;

            case Section.Rewind:
                tutGuide.RewindInfo();
                break;

            case Section.WallJump:
                tutGuide.WallJumpInfo();
                break;

            case Section.Attack:
                tutGuide.AttackInfo();
                break;
        }
    }
}
