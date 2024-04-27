using UnityEngine;

public class RescueNeeded : BaseRescueClass
{
    public enum HostageDensity : uint
    {
        Undefined = 0,
        VeryLow = 10,
        Low = 20,
        Normal = 30,
        Medium = 40,
        High = 50
    }

    public HostageDensity hostageDensity;

    [SerializeField]
    private uint hostageCount = (uint)HostageDensity.Undefined;

    void Start()
    {
        if(hostageDensity == HostageDensity.Undefined)
        {
            Debug.LogError("Did you setup hostage data correctly?");
        }

        if (hostageCount == 0)
        {
            hostageCount = (uint)hostageDensity;
        }

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
