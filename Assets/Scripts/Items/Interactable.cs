using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Range(1, 10)]
    public int Weight = 1;
    // Хранит позицию относительно гуся, к которой прикрепляется данный предмет при взаимодействии
    public Vector3 PointToHoldOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Outline>().enabled = false;
        }
    }
}
