using UnityEngine;

public class RescueDestination : BaseRescueClass
{
    public void Start()
    {
        if (this.transform.childCount != 1)
        {
            Debug.LogError("Did you setup destination object correctly?");
            return;
        }

        destinationPosition = this.transform.GetChild(0).transform.position;
    }

    private void OnMouseDown()
    {
        base.TriggerRescueEvent();
    }
}
