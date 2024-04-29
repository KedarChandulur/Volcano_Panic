using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    TMP_InputField inputField;
    UnityEngine.UI.Button button;
    TextMeshProUGUI textMeshProUGUI;
    GameObject errorTextGO;
    RescueVechicles rescueVechicle_Ref;
    RescueNeeded rescueNeeded_Ref;

    TextMeshProUGUI timerText;

    private void Awake()
    {
        if (!this.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out timerText))
        {
            Debug.LogError("Timer Text Ref not set");
            return;
        }
    }

    void Start()
    {
        Transform bg_Gameobject = this.transform.GetChild(0);
        bg_Gameobject.gameObject.SetActive(false);

        if (!bg_Gameobject.GetChild(0).TryGetComponent<TextMeshProUGUI>(out textMeshProUGUI))
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

    public void UpdateTimerDisplay(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnDestroy()
    {
        RescueEventHandler.OnReachingHostage -= RescueEventHandler_OnReachingHostage;
        button.onClick.RemoveAllListeners();
    }

    private void RescueEventHandler_OnReachingHostage(object sender, RescueEventHandler.CustomEventArgs customEventArgs)
    {
        rescueVechicle_Ref = null;
        rescueNeeded_Ref = null;

        if (customEventArgs.rescueNeeded.GetHostageCount() < 1)
        {
            return;
        }

        rescueVechicle_Ref = customEventArgs.rescueVechicle;
        rescueNeeded_Ref = customEventArgs.rescueNeeded;

        button.interactable = true;
        textMeshProUGUI.transform.parent.gameObject.SetActive(true);

        textMeshProUGUI.text = "How many hostages you want to save?\nHostages found at site: " + rescueNeeded_Ref.GetHostageCount();
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
        textMeshProUGUI.transform.parent.gameObject.SetActive(false);

        rescueNeeded_Ref.DecrementHostageCount(hostageCount);
        rescueVechicle_Ref.SetAgentState(RescueVechicles.AgentState.PickedUpHostages);

        inputField.text = string.Empty;
    }

    private IEnumerator ShowErrorText()
    {
        errorTextGO.SetActive(true);

        yield return new WaitForSeconds(2f);

        errorTextGO.SetActive(false);
    }
}
