using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public class Custom_UIManager_EventArgs : EventArgs
    {
        public uint hostageCount { get; }
        public int childTargetID { get; }

        public Custom_UIManager_EventArgs(uint _hostageCount, int _childTargetID)
        {
            hostageCount = _hostageCount;
            childTargetID = _childTargetID;
        }
    }

    TMP_InputField inputField;
    UnityEngine.UI.Button rescueButton;
    UnityEngine.UI.Button resumeButton_Pausemenu;
    UnityEngine.UI.Button quitButton_Pausemenu;

    TextMeshProUGUI hostageSaveText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI hostagesSavedText;
    TextMeshProUGUI errorText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI hostageStatusText;

    RescueNeeded rescueNeeded_Ref;
    RescueVechicles rescueVechile_Ref;

    GameObject pauseMenu;

    public static event EventHandler<Custom_UIManager_EventArgs> OnRescueButtonClickedEvent;

    float totalTime = 0.0f;
    uint totalHostages = 0;

    bool isPaused = false;

    private void Awake()
    {
        if (!this.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out timerText))
        {
            Debug.LogError("Timer Text Ref not set");
            return;
        }

        if(!this.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out hostagesSavedText))
        {
            Debug.LogError("Hostages Saved Text Ref not set");
            return;
        }
        
        if(!this.transform.GetChild(3).TryGetComponent<TextMeshProUGUI>(out scoreText))
        {
            Debug.LogError("Score Text Ref not set");
            return;
        }
        
        if(!this.transform.GetChild(4).TryGetComponent<TextMeshProUGUI>(out hostageStatusText))
        {
            Debug.LogError("Hostage Pickup Text Ref not set");
            return;
        }
        else
        {
            hostageStatusText.text = string.Empty;
        }

        pauseMenu = this.transform.GetChild(5).gameObject;

        if(pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Error settings the pause menu gameobject.");
        }

        if(!pauseMenu.transform.GetChild(0).TryGetComponent<UnityEngine.UI.Button>(out resumeButton_Pausemenu))
        {
            Debug.LogError("Error settings resume button.");
        }

        if (!pauseMenu.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out quitButton_Pausemenu))
        {
            Debug.LogError("Error settings quit button.");
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

        resumeButton_Pausemenu.onClick.RemoveAllListeners();
        resumeButton_Pausemenu.onClick.AddListener(FlipPauseFunctionality);

        quitButton_Pausemenu.onClick.RemoveAllListeners();
        quitButton_Pausemenu.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        RescueEventHandler.OnReachingHostage -= RescueEventHandler_OnReachingHostage;
        rescueButton.onClick.RemoveAllListeners();
        resumeButton_Pausemenu.onClick.RemoveAllListeners();
        quitButton_Pausemenu.onClick.RemoveAllListeners();
    }

    public void InitTotalTime(float _totalTime)
    {
        totalTime = _totalTime;

        Update_Tick(totalTime);
    }

    public void InitTotalHostagesCount(uint _totalHostagesCount)
    {
        totalHostages = _totalHostagesCount;

        UpdateHostagesSaved(0);
    }

    public void Update_Tick(float currentTime)
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

        //ScoreManager.UpdateTick();
    }

    public void UpdateHostagesSaved(uint hostagesSaved)
    {
        hostagesSavedText.text = "Hostages Saved: " + hostagesSaved + "\nHostages Left: " + (totalHostages - hostagesSaved);

        if(hostagesSaved > 0)
        {
            hostageStatusText.color = Color.green;
            StartCoroutine(ShowHostageStatusText("Hostages Rescued.")); 
        }
    }

    public void UpdateScore(uint currentScore)
    {
        scoreText.text = "Score: " + currentScore;
    }

    private void RescueEventHandler_OnReachingHostage(object sender, RescueEventHandler.Custom_RescueEventHandler_EventArgs customEventArgs)
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

        hostageSaveText.text = "How many hostages you want to save?\nHostages found: " + rescueNeeded_Ref.GetHostageCount() + "\nVechicle Capacity: " + rescueVechile_Ref.GetCurrentVechileCapacity();
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

        if(hostageCount > rescueVechile_Ref.GetCurrentVechileCapacity())
        {
            inputField.text = string.Empty;
            StartCoroutine(ShowErrorText("Can't select more hostages than vechicle capacity."));
            return;
        }

        rescueButton.interactable = false;
        hostageSaveText.transform.parent.gameObject.SetActive(false);

        ScoreManager.instance.IncrementPossibleScore(rescueNeeded_Ref.GetScoreUpdateBasedOnPriority((uint)hostageCount));

        ScoreManager.instance.IncreaseHostageSaveCount((uint)hostageCount);
        rescueVechile_Ref.DecrementCurrentVechicleCapacity((uint)hostageCount);

        hostageStatusText.color = rescueNeeded_Ref.GetColorBasedOnPriority();
        StartCoroutine(ShowHostageStatusText("Picked up Hostages."));

        OnRescueButtonClickedEvent?.Invoke(this, new Custom_UIManager_EventArgs((uint)hostageCount, rescueNeeded_Ref.GetChildObjectId()));

        inputField.text = string.Empty;
    }

    private IEnumerator ShowErrorText(string _errorText)
    {
        errorText.text = _errorText;

        yield return new WaitForSeconds(2f);

        errorText.text = string.Empty;
    }

    private IEnumerator ShowHostageStatusText(string _text)
    {
        hostageStatusText.text = _text;

        yield return new WaitForSeconds(2f);

        hostageStatusText.text = string.Empty;
    }

    public void FlipPauseFunctionality()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        if(isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
