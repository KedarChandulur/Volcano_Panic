using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    Scene currentActiveScene;

    NavMeshAgent agent;
    AgentState agentState = AgentState.Undefined;
    int targetChildID;

    private uint vechicleCapacity = 20;
    private uint currentVechicleCapacity = 0;

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
            this.ResetVechile();

            agentState = AgentState.Initialized;
        }

        BaseRescueClass.RescueEvent += BaseRescueClass_RescueEvent;
        UIManager.OnRescueButtonClickedEvent += UIManager_OnRescueButtonClickedEvent;
        //SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        //agent.ResetPath();

        //this.transform.position = Vector3.zero;
        //this.transform.eulerAngles = Vector3.zero;

        //this.transform.position = new Vector3(GameManager.instance.GetPlayerPosition().x, GameManager.instance.GetPlayerPosition().y, GameManager.instance.GetPlayerPosition().z);
        //this.transform.eulerAngles = new Vector3(GameManager.instance.GetPlayerRotation().x, GameManager.instance.GetPlayerRotation().y, GameManager.instance.GetPlayerRotation().z);

        //agent.ResetPath();
    }

    private void OnDestroy()
    {
        BaseRescueClass.RescueEvent -= BaseRescueClass_RescueEvent;
        UIManager.OnRescueButtonClickedEvent -= UIManager_OnRescueButtonClickedEvent;
        //SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    private void UIManager_OnRescueButtonClickedEvent(object sender, UIManager.Custom_UIManager_EventArgs e)
    {
        this.SetAgentState(AgentState.PickedUpHostages);
    }

    private void BaseRescueClass_RescueEvent(object sender, int _targetChildID)
    {
        BaseRescueClass baseRescue = (BaseRescueClass)sender;

        if (agentState == AgentState.PickedUpHostages)
        {
            if (baseRescue.GetType() != typeof(RescueDestination) && this.currentVechicleCapacity < 1)
            {
                Debug.Log("You already have enough hostages.");
                return;
            }

            targetChildID = _targetChildID;

            if(baseRescue.GetType() == typeof(RescueDestination))
            { 
                agentState = AgentState.InTransit_TowardsDestination;
            }

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
            this.ResetVechile();
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

    public uint GetCurrentVechileCapacity()
    {
        return currentVechicleCapacity;
    }

    public void DecrementCurrentVechicleCapacity(uint value)
    {
        currentVechicleCapacity -= value;
    }

    public void ResetVechile()
    {
        this.agent.speed = 7.0f;
        currentVechicleCapacity = vechicleCapacity;
    }

    //private void SceneManager_activeSceneChanged(Scene arg0, Scene changedTo)
    //{
    //    currentActiveScene = changedTo;

    //    if(currentActiveScene.buildIndex == 1)
    //    {   

    //    }
    //}
}
