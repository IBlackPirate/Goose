using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkInGardenAction : GoapAction
{
    public float Duration;
    public float LastWalkTime;

    private bool visited;
    private float startTime;
    private List<GardenBed> gardens;

    public WalkInGardenAction()
    {
        AddPrecondition("NeedVisitGarden", true);
        AddEffect("GardenVisited", true);
        AddEffect("HaveFun", true);
    }

    private void Start()
    {
        gardens = FindObjectsOfType<GardenBed>().ToList();
    }

    public override bool CheckProceduralPrecondition(GameObject agent)
    {
        //var index = Random.Range(0, gardens.Count);
        //target = gardens[index].gameObject;

        Tuple<GardenBed, float> finalTarget = Tuple.Create<GardenBed, float>(null, -500);
        foreach (var garden in gardens)
        {
            var distancePoints = -(garden.transform.position - transform.position).sqrMagnitude;
            var visitPoints = (Time.time - garden.LastVisitTime) * 40;
            var attractiveness = garden.Attractiveness * 300;

            var totalPoints = distancePoints + visitPoints + attractiveness;
            if (totalPoints > finalTarget.Item2)
                finalTarget = Tuple.Create(garden, totalPoints);
        }

        target = finalTarget.Item1.gameObject;
        return true;
    }

    public override bool IsDone()
    {
        return visited;
    }

    public override bool Perform(GameObject agent)
    {
        if (startTime == 0)
            startTime = Time.time;
        if (Time.time - startTime > Duration)
        {
            Saturn.Act(cost);
            visited = true;
            target.GetComponent<GardenBed>().LastVisitTime = Time.time + UnityEngine.Random.Range(-5, 5);
            LastWalkTime = Time.time;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        visited = false;
        startTime = 0;
    }
}
