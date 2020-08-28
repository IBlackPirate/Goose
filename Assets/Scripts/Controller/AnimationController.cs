using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    public bool IsDown { get; set; }

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

    public void Ga()
    {
        animator.Play("Ga");
    }

    public void HeadDown()
    {
        animator.SetBool("IsHeadDown", true);
    }

    public void HeadUp()
    {
        animator.SetBool("IsHeadDown", false);
    }

    public void RotateRight()
    {
        animator.Play("RotateRight");
    }

    public void RotateLeft()
    {
        animator.Play("RotateLeft");
    }

    public void RotateRightDown()
    {
        animator.Play("RotateRightDown");
    }

    public void RotateLeftDown()
    {
        animator.Play("RotateLeftDown");
    }
}
