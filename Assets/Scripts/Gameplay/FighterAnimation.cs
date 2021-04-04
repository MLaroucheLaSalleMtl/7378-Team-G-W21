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

    public void BlockAnimation(bool isBlocking)
    {
        animator.SetBool("IsBlocking", isBlocking);
    }

    public void SpecialAnimation()
    {
        switch (gameObject.GetComponent<FighterStatus>().playerID)
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
