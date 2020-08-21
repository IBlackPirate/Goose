using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterToCupAction : GoapAction
{
    public GameObject WaterPosition;
    public float Duration;

    private bool moved;
    private float startTime;

    public WaterToCupAction()
    {
        AddPrecondition("HasCup", true);
        AddEffect("WaterInCup", true);
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        target = WaterPosition;
        return true;
    }

    public override bool IsDone()
    {
        return moved;
    }

    public override bool Perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
        if(Time.time - startTime > Duration)
        {
            moved = true;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        moved = false;
        startTime = 0;
    }
}
