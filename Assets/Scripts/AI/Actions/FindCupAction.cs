using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCupAction : GoapAction
{
    public float GrabDistance;

    public Transform DefaultCupPlace;
    public Transform HandedCupPlace;
    public GameObject Cup;


    private bool checkedDefaultPlace;
    private bool hasCup;

    public FindCupAction()
    {
        AddEffect("HasCup", true);
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        if (CheckCup())
        {
            hasCup = true;
            return true;
        }

        if (!checkedDefaultPlace)
            target = DefaultCupPlace.gameObject;
        else
        {
            target = Cup;
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
            return false;
        }

        if (Vector3.Distance(Cup.transform.position, transform.position) < GrabDistance)
        {
            Cup.transform.position = HandedCupPlace.position;
            Cup.transform.parent = HandedCupPlace;
            Cup.GetComponent<Rigidbody>().isKinematic = true;
            hasCup = true;
            checkedDefaultPlace = false;
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
