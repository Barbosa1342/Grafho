using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        setDying(false);
    }

    public void setDying(bool dying)
    {
        animator.SetBool("isDying", dying);
    }

    public bool getDying()
    {
        return animator.GetBool("isDying");
    }

    public void setMove(bool move)
    {
        animator.SetBool("isMoving", move);
    }
}
