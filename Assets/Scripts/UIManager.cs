using System;
using System.Collections;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TMP_InputField inputField;
    UnityEngine.UI.Button rescueButton;

    TextMeshProUGUI hostageSaveText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI errorText;

    RescueNeeded rescueNeeded_Ref;
    RescueVechicles rescueVechile_Ref;

    public static event EventHandler<uint> OnRescueButtonClickedEvent;

    float totalTime = 0.0f;
    uint totalHostages = 0;

    private void Awake()
    {
        if (!this.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out timerText))
        {
            Debug.LogError("Timer Text Ref not set");
            return;
        }

        if(!this.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out scoreText))
        {
            Debug.LogError("Score Text Ref not set");
            return;
        }
    }

    void Start()
    {
        Transform bg_Gameobject = this.transform.GetChild(0);
        bg_Gameobject.gameObject.SetActive(false);

        if (!bg_Gameobject.GetChild(0).TryGetComponent<TextMeshProUGUI>(out hostageSaveText))
        {
            Debug.LogError("Text element not set");
            return;
        }

        if (!bg_Gameobject.GetChild(1).TryGetComponent<TMP_InputField>(out inputField))
        {
            Debug.LogError("Input Field element not set");
            return;
        }

        if (!bg_Gameobject.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out rescueButton))
        {
            Debug.LogError("Rescue Button element not set");
            return;
        }

        if(!bg_Gameobject.GetChild(3).TryGetComponent<TextMeshProUGUI>(out errorText))
        {
            Debug.LogError("Text Object element not set");
            return;
        }
        else
        {
            errorText.text = string.Empty;
        }

        RescueEventHandler.OnReachingHostage += RescueEventHandler_OnReachingHostage;

        rescueButton.onClick.RemoveAllListeners();
        rescueButton.onClick.AddListener(OnRescueButtonClicked);
    }

    private void OnDestroy()
    {
        RescueEventHandler.OnReachingHostage -= RescueEventHandler_OnReachingHostage;
        rescueButton.onClick.RemoveAllListeners();
    }

    public void InitTotalTime(float _totalTime)
    {
        totalTime = _totalTime;

        UpdateTimerDisplay(totalTime);
    }

    public void InitTotalHostagesCount(uint _totalHostagesCount)
    {
        totalHostages = _totalHostagesCount;

        UpdateScore(0);
    }

    public void UpdateTimerDisplay(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        if (minutes == 0)
        {
            if (Mathf.FloorToInt(currentTime % 60f) == 30)
            {
                timerText.color = Color.red;
            }
        }

        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(uint hostagesSaved)
    {
        scoreText.text = "Hostages Saved: " + (totalHostages - hostagesSaved) + "\nHostages Left: " + hostagesSaved;
    }

    private void RescueEventHandler_OnReachingHostage(object sender, RescueEventHandler.CustomEventArgs customEventArgs)
    {
        rescueNeeded_Ref = null;
        rescueVechile_Ref = null;

        if (customEventArgs.rescueNeeded.GetHostageCount() < 1)
        {
            return;
        }

        rescueNeeded_Ref = customEventArgs.rescueNeeded;
        rescueVechile_Ref = customEventArgs.rescueVechicle;

        errorText.text = string.Empty;
        rescueButton.interactable = true;
        hostageSaveText.transform.parent.gameObject.SetActive(true);

        hostageSaveText.text = "How many hostages you want to save?\nHostages found at site: " + rescueNeeded_Ref.GetHostageCount() + "\nVechicle Capacity: " + rescueVechile_Ref.GetVechicleCapacity();
    }

    public void OnRescueButtonClicked()
    {
        if(inputField.text == string.Empty)
        {
            return;
        }

        if (!int.TryParse(inputField.text, out int hostageCount))
        {
            Debug.Log("int Parse not Successful");
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText("Did you pass a number?"));
            return;
        }


        if (hostageCount < 0)
        {
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText("Enter number greater than 0."));
            return;
        }

        if(hostageCount > rescueNeeded_Ref.GetHostageCount())
        {
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText("Can't select more hostages than available."));
            return;
        }

        if(hostageCount > rescueVechile_Ref.GetVechicleCapacity())
        {
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText("Can't select more hostages than vechicle capacity."));
            return;
        }

        rescueButton.interactable = false;
        hostageSaveText.transform.parent.gameObject.SetActive(false);

        GameManager.instance.DecrementHostageCount((uint)hostageCount);
        OnRescueButtonClickedEvent?.Invoke(this, (uint)hostageCount);

        inputField.text = string.Empty;
    }

    private IEnumerator ShowErrorText(string _errorText)
    {
        errorText.text = _errorText;

        yield return new WaitForSeconds(2f);

        errorText.text = string.Empty;
    }
}
