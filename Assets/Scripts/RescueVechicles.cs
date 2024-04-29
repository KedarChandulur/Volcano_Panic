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
    int targetChildID;

    void Start()
    {
        if (agentState == AgentState.Undefined)
        {
            if(!this.TryGetComponent<NavMeshAgent>(out this.agent))
            {
                Debug.LogError("Error setting the agent.");
                return;
            }

            this.agent.autoRepath = true;
            this.agent.speed = 7.0f;

            agentState = AgentState.Initialized;
        }

        BaseRescueClass.RescueEvent += BaseRescueClass_RescueEvent;
        UIManager.OnRescueButtonClickedEvent += UIManager_OnRescueButtonClickedEvent;
    }

    private void OnDestroy()
    {
        BaseRescueClass.RescueEvent -= BaseRescueClass_RescueEvent;
        UIManager.OnRescueButtonClickedEvent -= UIManager_OnRescueButtonClickedEvent;
    }

    private void UIManager_OnRescueButtonClickedEvent(object sender, uint e)
    {
        this.SetAgentState(AgentState.PickedUpHostages);
    }

    private void BaseRescueClass_RescueEvent(object sender, int _targetChildID)
    {
        BaseRescueClass baseRescue = (BaseRescueClass)sender;

        if (agentState == AgentState.PickedUpHostages)
        {
            if (baseRescue.GetType() != typeof(RescueDestination))
            {
                Debug.Log("You already have enough hostages.");
                return;
            }

            targetChildID = _targetChildID;

            agentState = AgentState.InTransit_TowardsDestination;

            Debug.Log(agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }

        if (agentState == AgentState.Rescued || agentState == AgentState.Initialized)
        {
            if (baseRescue.GetType() != typeof(RescueNeeded))
            {
                Debug.Log("First Rescue some hostages.");
                return;
            }

            targetChildID = _targetChildID;

            agentState = AgentState.InTransit_TowardsHostage;

            Debug.Log(agentState);

            agent.SetDestination(baseRescue.GetDestination());
        }
    }

    public int GetTargetChildID()
    {
        return targetChildID;
    }

    public AgentState GetAgentState()
    {
        return agentState;
    }

    public void SetAgentState(AgentState value)
    {
        this.agentState = value;

        if(this.agentState == AgentState.PickedUpHostages)
        {
            this.agent.speed = 5.0f;
        }
        else if (this.agentState == AgentState.Rescued)
        {
            this.agent.speed = 7.0f;
        }
    }

    public void StopAgent()
    {
        if(agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}
