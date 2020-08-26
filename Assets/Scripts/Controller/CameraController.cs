using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;

    public float ZoomSpeed = 3f;
    public float ZoomScrollSpeed = 40f;
    public float RotationSpeed = 60f;

    public float MinAngle = -60f;
    public float MaxAngle = -5f;

    public float MinZoom = 5f;
    public float MaxZoom = 9f;
    public float currentZoom;
    public float previousZoom;

    private float currentAngle;

    private Vector3 prev;

    void Start()
    {
        currentAngle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        ApplyZoom();
        ApplyRotation();

        transform.position += Target.transform.position - prev;
        prev = Target.transform.position;
    }

    #region Moving&Rotation

    private void ApplyRotation()
    {
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            // Выбор точки, вокруг которой вращаем
            Vector3 point = Target.position;

            // Вертикальное вращение
            var inputY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime / Time.timeScale;
            currentAngle += inputY;
            if (currentAngle < MinAngle || currentAngle > MaxAngle)
            {
                currentAngle = Mathf.Clamp(currentAngle, MinAngle, MaxAngle);
            }
            else
            {
                transform.RotateAround(point, -transform.right, inputY);
            }

            // Горизонатальное вращение
            float inputX = 0;
            if (Input.GetKey(KeyCode.Q))
            {
                inputX = 10 * RotationSpeed * Time.deltaTime / Time.timeScale;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                inputX = -10 * RotationSpeed * Time.deltaTime / Time.timeScale;
            }
            else
            {
                inputX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime / Time.timeScale;
            }

            transform.RotateAround(point, Vector3.up, inputX);
        }
    }


    private void ApplyZoom()
    {
        float zoom = 0;

        if (Input.GetMouseButton(2))
        {
            var deltaY = Input.GetAxis("Mouse Y");
            zoom =  ZoomSpeed * Time.deltaTime * deltaY;
        }
        else
        {
            var heigth = Input.GetAxis("Mouse ScrollWheel");
            zoom =  ZoomScrollSpeed * Time.deltaTime * heigth;
        }
        if (zoom != 0)
        {
            previousZoom = currentZoom;
            currentZoom += zoom;
            currentZoom = Mathf.Clamp(currentZoom, MinZoom, MaxZoom);
            transform.position += transform.forward * (currentZoom - previousZoom);
            transform.LookAt(Target);
        }
    }
    #endregion
}