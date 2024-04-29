using System.Diagnostics;
using UnityEngine;

public class BaseRescueClass : UnityEngine.MonoBehaviour
{
    public static event System.EventHandler<int> RescueEvent;
    protected UnityEngine.Material arrow;
    protected UnityEngine.Vector3 destinationPosition;
    protected int _childObjectId;

    public UnityEngine.Vector3 GetDestination()
    { 
        return this.destinationPosition; 
    }

    public int GetChildObjectId()
    {
        return _childObjectId;
    }

    private void OnMouseDown()
    {
        RescueEvent?.Invoke(this, _childObjectId);
    }

    protected bool Initialize()
    {
        bool result = true;

        if (this.transform.childCount != 1)
        {
            UnityEngine.Debug.LogError("Did you setup destination object correctly?");
            result = false;
        }
        else
        {
            UnityEngine.Transform childTransform = this.transform.GetChild(0);

            _childObjectId = childTransform.GetInstanceID();

            destinationPosition = childTransform.position;

            MeshRenderer meshRenderer = null;
            if (childTransform.GetChild(0).TryGetComponent<UnityEngine.MeshRenderer>(out meshRenderer))
            {
                arrow = meshRenderer.material;
            }
            else
            {
                UnityEngine.Debug.LogError("Mesh Renderer not found.");
                result = false;
            }
        }

        return result;
    }
}