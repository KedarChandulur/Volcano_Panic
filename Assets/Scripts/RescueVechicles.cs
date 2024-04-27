using UnityEngine;
using UnityEngine.AI;

public class RescueVechicles : MonoBehaviour
{
    private enum AgentState
    {
        Undefined = -1,
        Initialized = 0,
        InTransit = 1,
        ReachedHostage = 2,
        PickedUpHostage = 3,
        ReachingDestination = 4,
        Rescued = 5
    }

    NavMeshAgent agent;
    AgentState agentState = AgentState.Undefined;

    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();

        BaseRescueClass.RescueEvent += BaseRescueClass_RescueEvent;

        if(agentState == AgentState.Undefined)
        {
            agentState = AgentState.Initialized;

            Debug.Log("Agent Initialized");
        }
    }

    private void OnDestroy()
    {
        BaseRescueClass.RescueEvent -= BaseRescueClass_RescueEvent;
    }

    private void BaseRescueClass_RescueEvent(object sender, System.EventArgs e)
    {
        BaseRescueClass baseRescue = (BaseRescueClass)sender;

        Debug.Log("Destination: " + baseRescue.GetDestination());

        if (agentState == AgentState.PickedUpHostage)
        {
            if (baseRescue.GetType() != typeof(RescueDestination))
            {
                Debug.Log("RescueDestination: Type didn't match.");
                return;
            }

            Debug.Log("Agent is moving.");
            Debug.Log(agentState);

            agentState = AgentState.InTransit;

            Debug.Log(agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }

        if (agentState == AgentState.Rescued || agentState == AgentState.Initialized)
        {
            if (baseRescue.GetType() != typeof(RescueNeeded))
            {
                Debug.Log("RescueNeeded: Type didn't match.");
                return;
            }

            Debug.Log("Agent is moving.");
            Debug.Log(agentState);

            agentState = AgentState.InTransit;

            Debug.Log(agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }
    }
}
