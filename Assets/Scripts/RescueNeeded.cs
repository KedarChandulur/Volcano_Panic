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
    private uint localHostageCount = (uint)HostageDensity.Undefined;

    private void Awake()
    {
        if (hostageDensity == HostageDensity.Undefined)
        {
            Debug.LogError("Did you setup hostage data correctly?");
        }

        if (localHostageCount == 0)
        {
            localHostageCount = (uint)hostageDensity;
        }

        if(GameObject.FindGameObjectWithTag("GameController").TryGetComponent<GameManager>(out GameManager gameManager))
        {
            gameManager.UpdateTotalHostageCount_Init(localHostageCount);
        }
    }

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

        UIManager.OnRescueButtonClickedEvent += UIManager_OnRescueButtonClickedEvent;
    }

    private void OnDestroy()
    {
        UIManager.OnRescueButtonClickedEvent -= UIManager_OnRescueButtonClickedEvent;
    }

    private void UIManager_OnRescueButtonClickedEvent(object sender, UIManager.Custom_UIManager_EventArgs e)
    {
        if(e.childTargetID == this.GetChildObjectId())
        { 
            this.DecrementHostageCount(e.hostageCount);
        }
    }

    public uint GetHostageCount()
    {
        return localHostageCount;
    }

    public void DecrementHostageCount(uint hostageCountToReduce)
    {
        this.localHostageCount -= hostageCountToReduce;
    }
}
