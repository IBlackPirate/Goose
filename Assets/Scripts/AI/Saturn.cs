using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saturn : MonoBehaviour, IGoap
{
    public static Saturn Instance;
    public float DefaultEnergy = 80;
    public float PathCompleteDistance;

    private NavMeshAgent agent;
    private WalkInGardenAction gardenAction;
    private RestAction restAction;
    private float energy;

    public static HashSet<GardenBed> NeedWatering;

    public static void OnRest()
    {
        Instance.energy = Instance.DefaultEnergy;
    }

    public static void Act(float cost)
    {
        Instance.energy -= cost;
    }

    private void Start()
    {
        Instance = this;
        NeedWatering = new HashSet<GardenBed>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        gardenAction = gameObject.GetComponent<WalkInGardenAction>();
        restAction = gameObject.GetComponent<RestAction>();
    }

    public void ActionsFinished()
    {

    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        if (energy <= 0)
        {
            goal.Add(new KeyValuePair<string, object>("HasRest", true));
        }
        else if (NeedWatering.Count > 0)
        {
            goal.Add(new KeyValuePair<string, object>("GardenWatered", true));
        }

        goal.Add(new KeyValuePair<string, object>("HaveFun", true));

        return goal;
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("NeedVisitGarden", true));
        worldData.Add(new KeyValuePair<string, object>("NeedRest", true));
        return worldData;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        if (agent.destination != nextAction.target.transform.position)
        {
            agent.SetDestination(nextAction.target.transform.position);
            agent.isStopped = false;
        }
        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < PathCompleteDistance)
        {
            nextAction.setInRange(true);
            agent.isStopped = true;
            return true;
        }
        return false;
    }

    public void PlanAborted(GoapAction aborter)
    {

    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {

    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> actions)
    {

    }
}
