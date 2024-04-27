public class BaseRescueClass : UnityEngine.MonoBehaviour
{
    public static event System.EventHandler RescueEvent;
    protected UnityEngine.Vector3 destinationPosition;

    public UnityEngine.Vector3 GetDestination()
    { 
        return this.destinationPosition; 
    }

    protected void TriggerRescueEvent()
    {
        RescueEvent?.Invoke(this, System.EventArgs.Empty);
    }
}