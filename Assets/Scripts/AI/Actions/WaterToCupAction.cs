using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterToCupAction : GoapAction
{
    public GameObject WaterPosition;
    public float Duration;

    private bool moved;
    private float startTime;
    private Cup cup;

    private FindCupAction findCupAction;

    private void Awake()
    {
        findCupAction = gameObject.GetComponent<FindCupAction>();
        cup = findCupAction.Cup;
    }

    public WaterToCupAction()
    {
        AddPrecondition("HasCup", true);
        AddEffect("WaterInCup", true);
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        if (Saturn.NeedWatering.Count == 0 || Saturn.Instance.Energy <= 0)
            return false;

        if (cup.GardenWateringCount > 0 && findCupAction.CheckCup())
        {
            moved = true;
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.WaterCan);
        }
        else
        {
            target = WaterPosition;
        }
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
            cup.OnWaterGrab();
            moved = true;
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.WaterCan);
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        if (moved)
            return false;
        return true;
    }

    public override void Reset()
    {
        moved = false;
        startTime = 0;
    }
}
