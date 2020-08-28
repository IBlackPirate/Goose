using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public float UpPosY = 0.54f;
    public float DownPosY = 0.1f;
    public int MaxWeight = 5;
    public GameObject GooseBeak;

    public MovingController GooseMovingController;
    public AnimationController AnimationController;

    private GameObject objToInteract = null;
    private bool IsInteract = false;
    private bool tooHeavy = false;
    

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, UpPosY, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && objToInteract != null)
        {
            IsInteract = true;
            var interactableInfo = objToInteract.GetComponent<Interactable>();

            tooHeavy = interactableInfo.Weight > MaxWeight;
            objToInteract.transform.parent = GooseBeak.transform;
            objToInteract.GetComponent<Rigidbody>().isKinematic = true;
            objToInteract.transform.localPosition = interactableInfo.PointToHoldOn;
            //GooseMovingController.OnGrab(interactableInfo.Weight);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && objToInteract != null)
        {
            objToInteract.transform.parent = null;
            objToInteract.GetComponent<Rigidbody>().isKinematic = false;
            objToInteract = null;
            IsInteract = false;
            tooHeavy = false;
            //GooseMovingController.OnThrow();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            while (Input.GetKey(KeyCode.LeftControl) && transform.position.y > DownPosY)
            {
                transform.Translate(Vector3.down * Time.deltaTime);
            }
            AnimationController.HeadDown();
            AnimationController.IsDown = true;
        }
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            while (transform.position.y < UpPosY && !(IsInteract && tooHeavy))
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            AnimationController.HeadUp();
            AnimationController.IsDown = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Interactable>() != null)
            objToInteract = other.transform.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!IsInteract)
        objToInteract = null;
    }
}
