using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private int characterNumber;

    public void DeadAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void PunchAnimation()
    {
        animator.SetTrigger("AttackOne");
    }

    public void KickAnimation()
    {
        animator.SetTrigger("AttackTwo");
    }

    public void HeadHitAnimation()
    {

    }

    public void TorsoHitAnimation()
    {
        animator.SetTrigger("isHit");
    }

    public void JumpAnimation()
    {

    }

    public void BlockAnimation()
    {
        animator.SetBool("isBlocking 0", true);
    }

    public void StopTheBlock()
    {
        animator.SetBool("isBlocking 0", false);
    }

    public void SpecialMove()
    {
        switch (characterNumber)
        {
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
