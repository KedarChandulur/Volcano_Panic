using UnityEngine;

public class RescueNeeded : BaseRescueClass
{
    public enum HostageDensity : uint
    {
        Undefined = 0,
        VeryLow = 1,
        Low = 2,
        Normal = 3,
        Medium = 4,
        High = 5
    }

    public HostageDensity hostagePriority;

    [SerializeField]
    private uint localHostageCount = 0;

    private void Awake()
    {
        if (hostagePriority == HostageDensity.Undefined || localHostageCount == 0)
        {
            Debug.LogError("Did you setup hostage data correctly?");
            return;
        }

        if(GameObject.FindGameObjectWithTag("ScoreController").TryGetComponent<ScoreManager>(out ScoreManager gameManager))
        {
            gameManager.UpdateTotalHostageCount_Init(localHostageCount);
        }
    }

    void Start()
    {
        if (base.Initialize())
        {
            switch(hostagePriority)
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

    public uint GetScoreUpdateBasedOnPriority(uint hostagesCount)
    {
        uint returnHostagesCount;

        switch (hostagePriority)
        {
            case HostageDensity.VeryLow:
                returnHostagesCount = hostagesCount * 2;
                break;
            case HostageDensity.Low:
                returnHostagesCount = hostagesCount * 3;
                break;
            case HostageDensity.Normal:
                returnHostagesCount = hostagesCount * 4;
                break;
            case HostageDensity.Medium:
                returnHostagesCount = hostagesCount * 5;
                break;
            case HostageDensity.High:
                returnHostagesCount = hostagesCount * 6;
                break;
            case HostageDensity.Undefined:
            default:
                returnHostagesCount = hostagesCount;
                Debug.LogError("Error setting the arrow color.");
                break;
        }

        return returnHostagesCount;
    }
}
