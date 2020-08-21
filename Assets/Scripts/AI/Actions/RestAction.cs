using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestAction : GoapAction
{
    public float Duration;
    public GameObject Bed;
    public float LastRestTime;

    private bool rested;
    private float startTime;

    public RestAction()
    {
        AddPrecondition("NeedRest", true);
        AddPrecondition("CupPlaced", true);
        AddEffect("HasRest", true);
        AddEffect("HaveFun", true);
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        target = Bed;
        return true;
    }

    public override bool IsDone()
    {
        return rested;
    }

    public override bool Perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
        if (Time.time - startTime > Duration)
        {
            LastRestTime = Time.time;
            rested = true;
            Saturn.OnRest();
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        rested = false;
        startTime = 0;
    }
}
