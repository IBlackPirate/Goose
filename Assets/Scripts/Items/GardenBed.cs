using System;
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
    public float MaxTimeWithoutWater = 15;
    public bool IsAlive => gameObject.activeInHierarchy;

    private MeshRenderer meshRenderer;
    private Color defaultColor;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        defaultColor = Color.Lerp(Color.green, Color.magenta, Attractiveness);
        meshRenderer.material.color = defaultColor;
    }

    private void Update()
    {
        if(Time.time - LastWaterTime > WateringFrequency)
        {
            Saturn.NeedWatering.Add(this);
            var t = (Time.time - LastWaterTime - WateringFrequency) / MaxTimeWithoutWater;
            meshRenderer.material.color = Color.Lerp(defaultColor, Color.black, t);

            if (t > 1)
            {
                RemoveGardenBed();
            }
        }
    }

    private void RemoveGardenBed()
    {
        Saturn.NeedWatering.Remove(this);
        gameObject.SetActive(false);
    }

    public void OnWatering()
    {
        meshRenderer.material.color = defaultColor;
        LastWaterTime = Time.time;
    }
}
