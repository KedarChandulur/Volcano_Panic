using UnityEngine;

public class EndGameUIManager : MonoBehaviour
{
    UnityEngine.UI.Button retryButton;
    UnityEngine.UI.Button mainMenuGamebutton;
    UnityEngine.UI.Button quitGamebutton;

    void Start()
    {
        GameObject gameEndScreen = this.transform.GetChild(0).gameObject;

        if (!gameEndScreen)
        {
            Debug.LogError("gameEndScreen Object element not set");
            return;
        }

        if (!gameEndScreen.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out retryButton))
        {
            Debug.LogError("Retry Button element not set");
            return;
        }
        
        if (!gameEndScreen.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out mainMenuGamebutton))
        {
            Debug.LogError("MainMenu Button element not set");
            return;
        }

        if (!gameEndScreen.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out quitGamebutton))
        {
            Debug.LogError("Quit Button element not set");
            return;
        }

        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        mainMenuGamebutton.onClick.RemoveAllListeners();
        mainMenuGamebutton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("StartGameScreen"); });

        quitGamebutton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.AddListener(() => { Debug.Log("Application Quit Called"); Application.Quit(); });
    }

    private void OnDestroy()
    {
        retryButton.onClick.RemoveAllListeners();
        mainMenuGamebutton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.RemoveAllListeners();
    }
}
