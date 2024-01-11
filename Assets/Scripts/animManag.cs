using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class animManag : MonoBehaviour
{
    public static animManag anim;
    [SerializeField] Animator animator;

    private void Awake()
    {
        anim = this;
    }

    // isAttacking is diferent than playerAttack.attack
    // both starts at the same time (mouse click)
    // but attack is immediate, while isAttacking
    // happens with the animation

    public void setAttackFalse()
    {
        animator.SetBool("isAttacking", false);

    }
    public void setAttackTrue()
    {
        animator.SetBool("isAttacking", true);
    }

    public bool getAttacking()
    {
       return animator.GetBool("isAttacking");
    }

    public void setMovement(float dirX, float dirY)
    {
        if (dirX != 0 || dirY != 0)
        {
            animator.SetBool("isMoving", true);

            if (dirX >= 0)
            {
                animator.SetBool("isRight", true);
            }
            else
            {
                animator.SetBool("isRight", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        animator.SetFloat("movementX", dirX);
        animator.SetFloat("movementY", dirY);
    }

    public void isAttackRight(float posX, float mousePosX)
    {
        if (posX >= mousePosX)
        {
            animator.SetBool("attackRight", false);
            animator.SetBool("isRight", false);
        }
        else
        {
            animator.SetBool("attackRight", true);
            animator.SetBool("isRight", true);
        }
    }
}
