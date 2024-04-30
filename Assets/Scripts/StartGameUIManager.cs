using UnityEngine;

public class StartGameUIManager : MonoBehaviour
{
    UnityEngine.UI.Button startGameButton;
    UnityEngine.UI.Button optionsButton;
    UnityEngine.UI.Button quitGamebutton;

    UnityEngine.UI.Button controlsButton;
    UnityEngine.UI.Button optionsBackButton;

    UnityEngine.UI.Button controlsBackButton;

    UnityEngine.UI.Button easyDifficultyButton;
    UnityEngine.UI.Button mediumDifficultyButton;
    UnityEngine.UI.Button hardDifficultyButton;
    UnityEngine.UI.Button startMenuBackButton;

    GameObject gameStartScreen;
    GameObject optionsScreen;
    GameObject controlsScreen;
    GameObject startGameMenu;

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

        startGameMenu = this.transform.GetChild(3).gameObject;

        if (!startGameMenu)
        {
            Debug.LogError("optionsScreen Object element not set");
            return;
        }
        else
        {
            startGameMenu.SetActive(false);
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
            Debug.LogError("Options Back Button element not set");
            return;
        }

        if (!controlsScreen.transform.GetChild(5).TryGetComponent<UnityEngine.UI.Button>(out controlsBackButton))
        {
            Debug.LogError("Controls Back Button element not set");
            return;
        }

        if (!startGameMenu.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out easyDifficultyButton))
        {
            Debug.LogError("Easy game Button element not set");
            return;
        }

        if (!startGameMenu.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out mediumDifficultyButton))
        {
            Debug.LogError("Medium game Button element not set");
            return;
        }

        if (!startGameMenu.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out hardDifficultyButton))
        {
            Debug.LogError("Hard game Button element not set");
            return;
        }

        if (!startGameMenu.transform.GetChild(4).TryGetComponent<UnityEngine.UI.Button>(out startMenuBackButton))
        {
            Debug.LogError("Start Menu Back Button element not set");
            return;
        }

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(() => { startGameMenu.SetActive(true); gameStartScreen.SetActive(false); });

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

        easyDifficultyButton.onClick.RemoveAllListeners();
        easyDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(360.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        mediumDifficultyButton.onClick.RemoveAllListeners();
        mediumDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(240.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        hardDifficultyButton.onClick.RemoveAllListeners();
        hardDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(120.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        startMenuBackButton.onClick.RemoveAllListeners();
        startMenuBackButton.onClick.AddListener(() => { startGameMenu.SetActive(false); gameStartScreen.SetActive(true); });
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.RemoveAllListeners();

        controlsButton.onClick.RemoveAllListeners();
        optionsBackButton.onClick.RemoveAllListeners();

        controlsBackButton.onClick.RemoveAllListeners();

        easyDifficultyButton.onClick.RemoveAllListeners();
        mediumDifficultyButton.onClick.RemoveAllListeners();
        hardDifficultyButton.onClick.RemoveAllListeners();
        startMenuBackButton.onClick.RemoveAllListeners();
    }
}
