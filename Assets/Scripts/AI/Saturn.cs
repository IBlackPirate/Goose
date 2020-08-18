using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Saturn : MonoBehaviour, IGoap
{
    public float PathCompleteDistance;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void ActionsFinished()
    {
        
    }

    public HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("GardenVisited", true));

        return goal;
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

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
