using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [Range(0, 1)]
    public float Attractiveness;
    public float WateringFrequency;

    public float LastVisitTime;
    public float LastWaterTime;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.green, Color.magenta, Attractiveness);
    }

    private void Update()
    {
        if(Time.time - LastWaterTime > WateringFrequency)
        {
            Saturn.NeedWatering.Add(this);
        }
    }
}
