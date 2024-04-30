using System;
using UnityEngine;

public class RescueEventHandler : MonoBehaviour
{
    public class CustomEventArgs : EventArgs
    {
        public RescueVechicles rescueVechicle { get; }
        public RescueNeeded rescueNeeded { get; }

        public CustomEventArgs(RescueVechicles _rescueVechicle, RescueNeeded _rescueNeeded)
        {
            rescueVechicle = _rescueVechicle;
            rescueNeeded = _rescueNeeded;
        }
    }

    private BaseRescueClass rescueClassRef;
    public static event EventHandler<CustomEventArgs> OnReachingHostage;

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

                GameManager.instance.UponReachingDestination();
            }

            if (rescueClassRef.GetType() == typeof(RescueNeeded) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsHostage)
            {
                rescueVechicle.SetAgentState(RescueVechicles.AgentState.ReachedHostage);

                OnReachingHostage?.Invoke(this, new CustomEventArgs(rescueVechicle, (RescueNeeded)rescueClassRef));

                rescueVechicle.StopAgent();
            }
        }
    }
}
