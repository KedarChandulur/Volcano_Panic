using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TMP_InputField inputField;
    UnityEngine.UI.Button button;

    TextMeshProUGUI hostageSaveText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI scoreText;

    GameObject errorTextGO;
    RescueNeeded rescueNeeded_Ref;

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

        if (!bg_Gameobject.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out button))
        {
            Debug.LogError("Button element not set");
            return;
        }

        errorTextGO = bg_Gameobject.GetChild(3).gameObject;

        if (errorTextGO)
        {
            errorTextGO.SetActive(false);
        }
        else
        {
            Debug.LogError("Error Text Object element not set");
            return;
        }

        RescueEventHandler.OnReachingHostage += RescueEventHandler_OnReachingHostage;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnRescueButtonClicked);
    }

    private void OnDestroy()
    {
        RescueEventHandler.OnReachingHostage -= RescueEventHandler_OnReachingHostage;
        button.onClick.RemoveAllListeners();
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

        if(Mathf.FloorToInt(currentTime % 60f) == 30)
        {
            timerText.color = Color.red;
        }

        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(uint hostagesSaved)
    {
        scoreText.text = "Hostages Saved: " + (totalHostages - hostagesSaved) + "\nHostages Left: " + hostagesSaved;
    }

    private void RescueEventHandler_OnReachingHostage(object sender, RescueNeeded rescueNeeded)
    {
        rescueNeeded_Ref = null;

        if (rescueNeeded.GetHostageCount() < 1)
        {
            return;
        }

        rescueNeeded_Ref = rescueNeeded;

        button.interactable = true;
        hostageSaveText.transform.parent.gameObject.SetActive(true);

        hostageSaveText.text = "How many hostages you want to save?\nHostages found at site: " + rescueNeeded_Ref.GetHostageCount();
    }

    public void OnRescueButtonClicked()
    {
        if(inputField.text == string.Empty)
        {
            return;
        }

        if (!uint.TryParse(inputField.text, out uint hostageCount))
        {
            Debug.Log("int Parse not Successful");
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText());
            return;
        }


        if (hostageCount < 0 || hostageCount > rescueNeeded_Ref.GetHostageCount())
        {
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText());
            return;
        }

        button.interactable = false;
        hostageSaveText.transform.parent.gameObject.SetActive(false);

        OnRescueButtonClickedEvent?.Invoke(this, hostageCount);

        inputField.text = string.Empty;
    }

    private IEnumerator ShowErrorText()
    {
        errorTextGO.SetActive(true);

        yield return new WaitForSeconds(2f);

        errorTextGO.SetActive(false);
    }
}
