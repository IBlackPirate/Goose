using UnityEngine;

public class MovingController : MonoBehaviour
{
    public float MoveSpeed = 0.7f;

    AnimationController animationController;

    private void Start()
    {
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

            // Anim Rotate
        }

    }

}

