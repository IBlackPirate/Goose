using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMove()
    {
        animator.SetBool("IsWalk", true);
    }

    public void StopMove()
    {
        animator.SetBool("IsWalk", false);
    }
}
