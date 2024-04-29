using Unity.VisualScripting;
using UnityEngine;
using static RescueNeeded;

public class RescueNeeded : BaseRescueClass
{
    public enum HostageDensity : int
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
    private int hostageCount = (int)HostageDensity.Undefined;

    void Start()
    {
        if (base.Initialize())
        {
            switch(hostageDensity)
            {
                case HostageDensity.VeryLow:
                    arrow.color = Color.cyan;
                    break;
                case HostageDensity.Low:
                    arrow.color = Color.blue;
                    break;
                case HostageDensity.Normal:
                    arrow.color = Color.white;
                    break;
                case HostageDensity.Medium:
                    arrow.color = Color.yellow;
                    break;
                case HostageDensity.High:
                    arrow.color = Color.red;
                    break;
                case HostageDensity.Undefined:
                default:
                    arrow.color = Color.grey;
                    Debug.LogError("Error setting the arrow color.");
                    break;
            }
        }
        else
        {
            Debug.LogError("Not able to set the arrow color");
        }

        if (hostageDensity == HostageDensity.Undefined)
        {
            Debug.LogError("Did you setup hostage data correctly?");
        }

        if (hostageCount == 0)
        {
            hostageCount = (int)hostageDensity;
        }
    }

    //private void OnMouseDown()
    //{
    //    base.TriggerRescueEvent();
    //}

    public int GetHostageCount()
    {
        return hostageCount;
    }

    public void DecrementHostageCount(int hostageCountToReduce)
    {
        this.hostageCount -= hostageCountToReduce;
    }
}
