using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saturn : MonoBehaviour, IGoap
{
    public static Saturn Instance;
    public AudioSource Steps;
    public float Angry;
    public float AngryRegen;
    public float DefaultEnergy = 80;
    public float PathCompleteDistance;

    private NavMeshAgent agent;
    private WalkInGardenAction gardenAction;
    private RestAction restAction;
    public float Energy { get; private set; }

    public static HashSet<GardenBed> NeedWatering;

    public static void OnRest()
    {
        Instance.Energy = Instance.DefaultEnergy;
    }

    public static void Act(float cost)
    {
        Instance.Energy -= cost;
    }

    private void Start()
    {
        Instance = this;
        NeedWatering = new HashSet<GardenBed>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        gardenAction = gameObject.GetComponent<WalkInGardenAction>();
        restAction = gameObject.GetComponent<RestAction>();
        OnRest();
    }

    private void Update()
    {
        Angry -= AngryRegen * Time.deltaTime;
        Angry = Mathf.Clamp(Angry, 0, 1);
    }

    public void ActionsFinished()
    {

    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        if (Energy <= 0)
        {
            goal.Add(new KeyValuePair<string, object>("HasRest", true));
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.Sleeping);
        }
        else if (NeedWatering.Count > 0)
        {
            goal.Add(new KeyValuePair<string, object>("GardenWatered", true));
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.FindWaterCan);
        }
        else
        {
            goal.Add(new KeyValuePair<string, object>("GardenVisited", true));
            Saturn_UI.Instance.SelectEmotion(Saturn_UI.Instance.Flowers);
        }

        return goal;
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
        worldData.Add(new KeyValuePair<string, object>("NeedVisitGarden", true));
        worldData.Add(new KeyValuePair<string, object>("NeedRest", true));
        worldData.Add(new KeyValuePair<string, object>("NeedWatering", NeedWatering.Count > 0));
        return worldData;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        if (!Steps.isPlaying)
            Steps.Play();
        if (agent.destination != nextAction.target.transform.position)
        {
            agent.SetDestination(nextAction.target.transform.position);
            agent.isStopped = false;
        }
        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < PathCompleteDistance)
        {
            nextAction.setInRange(true);
            agent.isStopped = true;
            Steps.Stop();
            return true;
        }
        Energy -= Time.deltaTime;
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
