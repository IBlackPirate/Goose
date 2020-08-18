using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    public float LastVisitTime;
    [Range(0, 1)]
    public float Attractiveness;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.green, Color.magenta, Attractiveness);
    }
}
