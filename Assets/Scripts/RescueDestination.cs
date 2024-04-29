using UnityEngine;

public class RescueDestination : BaseRescueClass
{
    public void Start()
    {
        if(base.Initialize())
        {
            arrow.color = Color.green;
        }
        else
        {
            Debug.LogError("Not able to set the arrow color");
        }
    }
}
