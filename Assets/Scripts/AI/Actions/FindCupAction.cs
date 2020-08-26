using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCupAction : GoapAction
{
    public float GrabDistance;
    public float CupNotFoundAngry = 0.15f;

    public Transform DefaultCupPlace;
    public Transform HandedCupPlace;
    public Cup Cup;


    private bool checkedDefaultPlace;
    private bool hasCup;

    public FindCupAction()
    {
        AddPrecondition("NeedWatering", true);
        AddEffect("HasCup", true);
    }      

    public override bool CheckProceduralPrecondition(GameObject agent)
    {

        if (Saturn.NeedWatering.Count == 0 || Saturn.Instance.Energy <= 0)
            return false;

        if (CheckCup())
        {
            hasCup = true;
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.FullWaterCan);
            return true;
        }

        if (!checkedDefaultPlace)
        {
            target = DefaultCupPlace.gameObject;
        }
        else
        {
            target = Cup.gameObject;
        }
        return true;
    }

    public bool CheckCup()
    {
        return Cup.transform.position == HandedCupPlace.position;
    }


    public override bool IsDone()
    {
        return hasCup;
    }

    public override bool Perform(GameObject agent)
    {
        if (!checkedDefaultPlace && Vector3.Distance(Cup.transform.position, DefaultCupPlace.position) > 1.5f)
        {
            checkedDefaultPlace = true;
            Saturn.Instance.Angry += CupNotFoundAngry;
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.NotFoundWaterCan);
            return false;
        }

        if (Vector3.Distance(Cup.transform.position, transform.position) < GrabDistance)
        {
            Cup.transform.position = HandedCupPlace.position;
            Cup.transform.parent = HandedCupPlace;
            Cup.GetComponent<Rigidbody>().isKinematic = true;
            hasCup = true;
            checkedDefaultPlace = false;
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.FullWaterCan);
        }

        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        hasCup = false;
    }
}
