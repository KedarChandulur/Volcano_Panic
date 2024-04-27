using System;
using UnityEngine;

public class RescueDestination : MonoBehaviour
{
    public static event EventHandler ShareDestination;
    public Vector3 destinationPosition;

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
        //Debug.Log("Rescue Event Triggered");

        ShareDestination?.Invoke(destinationPosition, EventArgs.Empty);
    }
}
