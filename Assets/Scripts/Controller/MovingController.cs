using System;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    public float MoveSpeed;

    private float baseMoveSpeed = 0.7f;
    private AnimationController animationController;

    private void Start()
    {
        MoveSpeed = baseMoveSpeed;
        animationController = GetComponent<AnimationController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        if(ver != 0)
        {
            var offset = new Vector3(0, 0, ver);

            transform.Translate(offset * MoveSpeed * Time.deltaTime);
            animationController.StartMove();
        }
        else
        {
            animationController.StopMove();
        }

        if (hor != 0)
        {
            transform.Rotate(Vector3.up, hor);

            if (hor > 0)
            {
                if (animationController.IsDown)
                    animationController.RotateRightDown();
                else
                    animationController.RotateRight();
            }

            else
            {
                if (animationController.IsDown)
                    animationController.RotateLeftDown();
                else
                    animationController.RotateLeft();
            }
            }
    }

    //// Меняем скорость в зависимости от веса схваченного предмета
    //public void OnGrab(int weight)
    //{
    //    MoveSpeed *= (10 - weight) / 20;
    //}

    //public void OnThrow()
    //{
    //    MoveSpeed = baseMoveSpeed;
    //}
}

