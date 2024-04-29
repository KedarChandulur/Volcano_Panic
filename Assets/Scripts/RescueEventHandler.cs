using System;
using UnityEngine;

public class RescueEventHandler : MonoBehaviour
{
    private BaseRescueClass rescueClassRef;
    public static event EventHandler<RescueNeeded> OnReachingHostage;
    public static event EventHandler OnReachingDestination;

    void Start()
    {
        rescueClassRef = GetComponentInParent<BaseRescueClass>();

        if(rescueClassRef == null)
        {
            Debug.Log("BaseRescueClass: Not Found in parent");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RescueVechicle"))
        {
            RescueVechicles rescueVechicle = other.GetComponent<RescueVechicles>();

            if(rescueClassRef.GetChildObjectId() != rescueVechicle.GetTargetChildID())
            {
                Debug.Log("Not reached target yet.");
                return;
            }

            if (rescueClassRef.GetType() == typeof(RescueDestination) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsDestination)
            {
                rescueVechicle.SetAgentState(RescueVechicles.AgentState.Rescued);

                OnReachingDestination?.Invoke(this, EventArgs.Empty);

                Debug.Log(rescueVechicle.GetAgentState());
            }

            if (rescueClassRef.GetType() == typeof(RescueNeeded) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsHostage)
            {
                rescueVechicle.SetAgentState(RescueVechicles.AgentState.ReachedHostage);

                OnReachingHostage?.Invoke(this, (RescueNeeded)rescueClassRef);

                rescueVechicle.StopAgent();

                Debug.Log(rescueVechicle.GetAgentState());
            }
        }
    }
}
