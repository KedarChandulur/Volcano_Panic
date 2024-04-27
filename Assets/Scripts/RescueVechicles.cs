using UnityEngine;
using UnityEngine.AI;

public class RescueVechicles : MonoBehaviour
{
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();
        RescueDestination.ShareDestination += RescueDestination_ShareDestination;
    }

    private void OnDestroy()
    {
        RescueDestination.ShareDestination -= RescueDestination_ShareDestination;
    }

    private void RescueDestination_ShareDestination(object sender, System.EventArgs e)
    {
        if (Mathf.Approximately(agent.remainingDistance, 0.0f))
        {
            Debug.Log(agent.remainingDistance);
            Debug.Log("Reached!");

            Vector3 destinationPosition = (Vector3)sender;
            agent.SetDestination(destinationPosition);
        }
    }
}
