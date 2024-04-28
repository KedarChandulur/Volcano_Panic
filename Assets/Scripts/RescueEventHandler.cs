using UnityEngine;

public class RescueEventHandler : MonoBehaviour
{
    private BaseRescueClass rescueClassRef;

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

            if (rescueClassRef.GetType() == typeof(RescueDestination) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsDestination)
            {
                Debug.Log(rescueVechicle.GetAgentState());

                rescueVechicle.SetAgentState(RescueVechicles.AgentState.Rescued);

                Debug.Log(rescueVechicle.GetAgentState());
            }

            if (rescueClassRef.GetType() == typeof(RescueNeeded) && rescueVechicle.GetAgentState() == RescueVechicles.AgentState.InTransit_TowardsHostage)
            {
                Debug.Log(rescueVechicle.GetAgentState());

                rescueVechicle.SetAgentState(RescueVechicles.AgentState.ReachedHostage);

                Debug.Log(rescueVechicle.GetAgentState());
            }
        }
    }
}
