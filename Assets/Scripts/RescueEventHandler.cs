using System;
using UnityEngine;

public class RescueEventHandler : MonoBehaviour
{
    public class CustomEventArgs : EventArgs
    {
        //public object Sender { get; }
        public RescueVechicles rescueVechicle { get; }
        public RescueNeeded rescueNeeded { get; }

        //public CustomEventArgs(object sender, RescueVechicles _rescueVechicle, RescueNeeded _rescueNeeded)
        public CustomEventArgs(RescueVechicles _rescueVechicle, RescueNeeded _rescueNeeded)
        {
            //Sender = sender;
            rescueVechicle = _rescueVechicle;
            rescueNeeded = _rescueNeeded;
        }
    }

    private BaseRescueClass rescueClassRef;
    public static event EventHandler<CustomEventArgs> OnReachingHostage;

    // Start is called before the first frame update
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
        Debug.Log("Triggered");

        if (other.gameObject.CompareTag("RescueVechicle"))
        {
            RescueVechicles rescueVechicle = other.GetComponent<RescueVechicles>();

            Debug.Log(rescueClassRef.GetType());
            Debug.Log(rescueVechicle.GetAgentState());

            if(rescueClassRef.GetChildObjectId() != rescueVechicle.GetTargetChildID())
            {
                Debug.Log("Not reached target yet.");
                return;
            }

            if (rescueClassRef.GetType() == typeof(RescueDestination) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsDestination)
            {
                rescueVechicle.SetAgentState(RescueVechicles.AgentState.Rescued);

                Debug.Log(rescueVechicle.GetAgentState());
            }

            if (rescueClassRef.GetType() == typeof(RescueNeeded) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsHostage)
            {
                rescueVechicle.SetAgentState(RescueVechicles.AgentState.ReachedHostage);

                OnReachingHostage?.Invoke(this, new CustomEventArgs(rescueVechicle, (RescueNeeded)rescueClassRef));

                rescueVechicle.StopAgent();

                Debug.Log(rescueVechicle.GetAgentState());
            }
        }
    }
}
