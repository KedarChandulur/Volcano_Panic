using UnityEngine;
using UnityEngine.AI;

public class RescueVechicles : MonoBehaviour
{
    public enum AgentState
    {
        Undefined = -1,
        Initialized = 0,
        InTransit_TowardsHostage = 1,
        ReachedHostage = 2,
        PickedUpHostages = 3,
        InTransit_TowardsDestination = 4,
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
            //Debug.Log("Agent not Initialized, Initializing...");

            agentState = AgentState.Initialized;
        }
    }

    private void OnDestroy()
    {
        BaseRescueClass.RescueEvent -= BaseRescueClass_RescueEvent;
    }

    private void BaseRescueClass_RescueEvent(object sender, System.EventArgs e)
    {
        BaseRescueClass baseRescue = (BaseRescueClass)sender;

        if (agentState == AgentState.PickedUpHostages)
        {
            if (baseRescue.GetType() != typeof(RescueDestination))
            {
                Debug.Log("You already have enough hostages.");
                return;
            }

            agentState = AgentState.InTransit_TowardsDestination;

            Debug.Log("Changing to State: " + agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }

        if (agentState == AgentState.Rescued || agentState == AgentState.Initialized)
        {
            if (baseRescue.GetType() != typeof(RescueNeeded))
            {
                Debug.Log("First Rescue some hostages.");
                return;
            }

            agentState = AgentState.InTransit_TowardsHostage;

            Debug.Log("Changing to State: " + agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }
    }

    public AgentState GetAgentState()
    {
        return agentState;
    }

    public void SetAgentState(AgentState value)
    {
        this.agentState = value;
    }
}
