using UnityEngine;

public class StartGameUIManager : MonoBehaviour
{
    UnityEngine.UI.Button startGameButton;
    UnityEngine.UI.Button optionsButton;
    UnityEngine.UI.Button quitGamebutton;

    UnityEngine.UI.Button controlsButton;
    UnityEngine.UI.Button optionsBackButton;

    UnityEngine.UI.Button controlsBackButton;

    GameObject gameStartScreen;
    GameObject optionsScreen;
    GameObject controlsScreen;

    void Start()
    {
        gameStartScreen = this.transform.GetChild(0).gameObject;

        if (!gameStartScreen)
        {
            Debug.LogError("gameStartScreen Object element not set");
            return;
        }
        else
        {
            gameStartScreen.SetActive(true);
        }

        optionsScreen = this.transform.GetChild(1).gameObject;

        if (!optionsScreen)
        {
            Debug.LogError("optionsScreen Object element not set");
            return;
        }
        else
        {
            optionsScreen.SetActive(false);
        }

        controlsScreen = this.transform.GetChild(2).gameObject;

        if (!controlsScreen)
        {
            Debug.LogError("optionsScreen Object element not set");
            return;
        }
        else
        {
            controlsScreen.SetActive(false);
        }

        if (!gameStartScreen.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out startGameButton))
        {
            Debug.LogError("Rety Button element not set");
            return;
        }

        if (!gameStartScreen.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out optionsButton))
        {
            Debug.LogError("Rety Button element not set");
            return;
        }

        if (!gameStartScreen.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out quitGamebutton))
        {
            Debug.LogError("Quit Button element not set");
            return;
        }

        if (!optionsScreen.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out controlsButton))
        {
            Debug.LogError("Controls Button element not set");
            return;
        }

        if (!optionsScreen.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out optionsBackButton))
        {
            Debug.LogError("Back Button element not set");
            return;
        }

        if (!controlsScreen.transform.GetChild(5).TryGetComponent<UnityEngine.UI.Button>(out controlsBackButton))
        {
            Debug.LogError("Back Button element not set");
            return;
        }

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        optionsButton.onClick.RemoveAllListeners();
        optionsButton.onClick.AddListener(() => { optionsScreen.SetActive(true); gameStartScreen.SetActive(false); });

        quitGamebutton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.AddListener(() => { Debug.Log("Application Quit Called"); Application.Quit(); });

        controlsButton.onClick.RemoveAllListeners();
        controlsButton.onClick.AddListener(() => { controlsScreen.SetActive(true); optionsScreen.SetActive(false); });

        optionsBackButton.onClick.RemoveAllListeners();
        optionsBackButton.onClick.AddListener(() => { optionsScreen.SetActive(false); gameStartScreen.SetActive(true); });

        controlsBackButton.onClick.RemoveAllListeners();
        controlsBackButton.onClick.AddListener(() => { controlsScreen.SetActive(false); optionsScreen.SetActive(true); });

    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.RemoveAllListeners();

        controlsButton.onClick.RemoveAllListeners();
        optionsBackButton.onClick.RemoveAllListeners();

        controlsBackButton.onClick.RemoveAllListeners();
    }
}
