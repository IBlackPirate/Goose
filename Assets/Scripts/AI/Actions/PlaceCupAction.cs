using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCupAction : GoapAction
{
    private FindCupAction findCupAction;
    private bool placed;

    public PlaceCupAction()
    {
        AddPrecondition("NeedRest", true);
        AddEffect("CupPlaced", true);
    }

    private void Start()
    {
        findCupAction = gameObject.GetComponent<FindCupAction>();
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        if (!findCupAction.CheckCup())
        {
            placed = true;
        }
        else target = findCupAction.DefaultCupPlace.gameObject;
        return true;
    }

    public override bool IsDone()
    {
        return placed;
    }

    public override bool Perform(GameObject agent)
    {
        findCupAction.Cup.transform.position = findCupAction.DefaultCupPlace.position;
        findCupAction.Cup.transform.parent = null;
        findCupAction.Cup.GetComponent<Rigidbody>().isKinematic = false;
        placed = true;
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        placed = false;
    }
}
