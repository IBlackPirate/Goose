﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterGardenAction : GoapAction
{
    public float Duration;

    private bool watered;
    private float startTime;
    private Cup cup;

    private FindCupAction findCupAction;

    private void Start()
    {
        findCupAction = gameObject.GetComponent<FindCupAction>();
        cup = findCupAction.Cup;
    }

    public WaterGardenAction()
    {
        AddPrecondition("WaterInCup", true);
        AddEffect("HaveFun", true);
        AddEffect("GardenWatered", true);
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        if (Saturn.NeedWatering.Count == 0)
            return false;
        target = Saturn.NeedWatering
            .OrderBy(x => x.LastWaterTime)
            .ThenBy(x => (x.transform.position - transform.position).sqrMagnitude)
            .First().gameObject;
        return true;
    }

    public override bool IsDone()
    {
        return watered;
    }

    public override bool Perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        var garden = target.GetComponent<GardenBed>();

        if (!garden.IsAlive)
            return false;

        if (!cup.WaterSpill.isPlaying)
        {
            cup.WaterSpill.Play();
            garden.OnWatering();
        }

        if (Time.time - startTime > Duration && !watered)
        {
            watered = true;
            cup.GardenWateringCount--;
            Saturn.NeedWatering.Remove(garden);
            Saturn.Act(cost);
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        watered = false;
        startTime = 0;
    }
}
