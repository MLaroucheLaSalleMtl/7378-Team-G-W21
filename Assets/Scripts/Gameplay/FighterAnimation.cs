using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nicholaos And Eduardo Worked on this Script 

public class FighterAnimation : MonoBehaviour
{
    [Header("Player Manager")]
    [SerializeField] private Animator animator;
    private MyCharacterController controller;
    private FighterStatus fighterStatus;

    private void Awake()
    {
        CastComponents();
    }

    private void CastComponents()
    {
        controller = gameObject.GetComponent<MyCharacterController>();
        fighterStatus = gameObject.GetComponent<FighterStatus>();
    }

    public void DeadAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void VictoryAnimation()
    {
        controller.VictoryWithNoControl();
        animator.SetBool("Victory", true);
    }

    public void PunchAnimation()
    {
        animator.SetTrigger("AttackOne");
    }

    public void KickAnimation()
    {
        animator.SetTrigger("AttackTwo");
    }

    public void TorsoHitAnimation()
    {
        animator.SetTrigger("isHit");
    }

    public void BlockAnimation(bool isBlocking)
    {
        animator.SetBool("IsBlocking", isBlocking);
    }

    public void BlockedHitAnimation()
    {
        animator.SetTrigger("BlockedHit");
    }

    public void SpecialAnimation()
    {
        switch (fighterStatus.playerID)
        {
            case 0:
                animator.SetTrigger("SpecialAttack0");
                break;
            case 1:
                animator.SetTrigger("SpecialAttack1");
                break;
            case 2:
                animator.SetTrigger("SpecialAttack0");
                break;
            default:
                break;
        }
    }
}
