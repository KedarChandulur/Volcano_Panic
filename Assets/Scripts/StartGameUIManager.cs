using UnityEngine;

public class StartGameUIManager : MonoBehaviour
{
    UnityEngine.UI.Button startGameButton;
    UnityEngine.UI.Button optionsButton;
    UnityEngine.UI.Button quitGamebutton;

    UnityEngine.UI.Button controlsButton;
    UnityEngine.UI.Button instructionsButton;
    UnityEngine.UI.Button scoresButton;
    UnityEngine.UI.Button optionsBackButton;

    UnityEngine.UI.Button controlsBackButton;

    UnityEngine.UI.Button easyDifficultyButton;
    UnityEngine.UI.Button mediumDifficultyButton;
    UnityEngine.UI.Button hardDifficultyButton;
    UnityEngine.UI.Button startMenuBackButton;

    UnityEngine.UI.Button instructionsMenuBackButton;

    TMPro.TextMeshProUGUI highScoreText;
    UnityEngine.UI.Button scoresMenuBackButton;

    //UnityEngine.UI.Button drugStoreButton;
    //UnityEngine.UI.Button stadiumButton;
    //UnityEngine.UI.Button factoryButton;
    //UnityEngine.UI.Button playersSpawnMenuBackButton;


    GameObject gameStartScreen;
    GameObject optionsScreen;
    GameObject controlsScreen;
    GameObject startGameMenu;
    GameObject instructionsMenu;
    GameObject scoresMenu;
    //GameObject playersSpawnMenu;

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

        instructionsMenu = this.transform.GetChild(4).gameObject;

        if (!instructionsMenu)
        {
            Debug.LogError("instructionsMenu Object element not set");
            return;
        }
        else
        {
            instructionsMenu.SetActive(false);
        }

        scoresMenu = this.transform.GetChild(5).gameObject;

        if (!scoresMenu)
        {
            Debug.LogError("instructionsMenu Object element not set");
            return;
        }
        else
        {
            scoresMenu.SetActive(false);
        }

        //playersSpawnMenu = this.transform.GetChild(6).gameObject;

        //if (!playersSpawnMenu)
        //{
        //    Debug.LogError("playersSpawnMenu Object element not set");
        //    return;
        //}
        //else
        //{
        //    playersSpawnMenu.SetActive(false);
        //}

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
        
        if (!optionsScreen.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out instructionsButton))
        {
            Debug.LogError("Instructions Button element not set");
            return;
        }
        
        if (!optionsScreen.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out scoresButton))
        {
            Debug.LogError("Scores Button element not set");
            return;
        }

        if (!optionsScreen.transform.GetChild(4).TryGetComponent<UnityEngine.UI.Button>(out optionsBackButton))
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
        
        if (!instructionsMenu.transform.GetChild(7).TryGetComponent<UnityEngine.UI.Button>(out instructionsMenuBackButton))
        {
            Debug.LogError("Instructions Menu Back Button element not set");
            return;
        }
        
        if (!scoresMenu.transform.GetChild(7).TryGetComponent<TMPro.TextMeshProUGUI>(out highScoreText))
        {
            Debug.LogError("High Score Text element not set");
            return;
        }
        
        if (!scoresMenu.transform.GetChild(8).TryGetComponent<UnityEngine.UI.Button>(out scoresMenuBackButton))
        {
            Debug.LogError("Scores Menu Back Button element not set");
            return;
        }
        
        //if (!playersSpawnMenu.transform.GetChild(1).TryGetComponent<UnityEngine.UI.Button>(out drugStoreButton))
        //{
        //    Debug.LogError("Drug store Spawn Button element not set");
        //    return;
        //}
        
        //if (!playersSpawnMenu.transform.GetChild(2).TryGetComponent<UnityEngine.UI.Button>(out stadiumButton))
        //{
        //    Debug.LogError("Stadium Spawn Button element not set");
        //    return;
        //}
        
        //if (!playersSpawnMenu.transform.GetChild(3).TryGetComponent<UnityEngine.UI.Button>(out factoryButton))
        //{
        //    Debug.LogError("Factory Spawn Button element not set");
        //    return;
        //}
        
        //if (!playersSpawnMenu.transform.GetChild(4).TryGetComponent<UnityEngine.UI.Button>(out playersSpawnMenuBackButton))
        //{
        //    Debug.LogError("Players Spawn Menu Back Button element not set");
        //    return;
        //}

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(() => { startGameMenu.SetActive(true); gameStartScreen.SetActive(false); });

        optionsButton.onClick.RemoveAllListeners();
        optionsButton.onClick.AddListener(() => { optionsScreen.SetActive(true); gameStartScreen.SetActive(false); });

        quitGamebutton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.AddListener(() => { Debug.Log("Application Quit Called"); Application.Quit(); });

        controlsButton.onClick.RemoveAllListeners();
        controlsButton.onClick.AddListener(() => { controlsScreen.SetActive(true); optionsScreen.SetActive(false); });

        instructionsButton.onClick.RemoveAllListeners();
        instructionsButton.onClick.AddListener(() => { instructionsMenu.SetActive(true); optionsScreen.SetActive(false); });

        scoresButton.onClick.RemoveAllListeners();
        scoresButton.onClick.AddListener(() => { scoresMenu.SetActive(true); optionsScreen.SetActive(false); });

        optionsBackButton.onClick.RemoveAllListeners();
        optionsBackButton.onClick.AddListener(() => { optionsScreen.SetActive(false); gameStartScreen.SetActive(true); });

        controlsBackButton.onClick.RemoveAllListeners();
        controlsBackButton.onClick.AddListener(() => { controlsScreen.SetActive(false); optionsScreen.SetActive(true); });

        easyDifficultyButton.onClick.RemoveAllListeners();
        easyDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(360.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; startGameMenu.SetActive(false); });

        mediumDifficultyButton.onClick.RemoveAllListeners();
        mediumDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(240.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; startGameMenu.SetActive(false); });

        hardDifficultyButton.onClick.RemoveAllListeners();
        hardDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(120.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; startGameMenu.SetActive(false); });        
        
        //easyDifficultyButton.onClick.RemoveAllListeners();
        //easyDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(360.0f); playersSpawnMenu.SetActive(true); startGameMenu.SetActive(false); });

        //mediumDifficultyButton.onClick.RemoveAllListeners();
        //mediumDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(240.0f); playersSpawnMenu.SetActive(true); startGameMenu.SetActive(false); });

        //hardDifficultyButton.onClick.RemoveAllListeners();
        //hardDifficultyButton.onClick.AddListener(() => { GameManager.instance.SetGameTime(120.0f); playersSpawnMenu.SetActive(true); startGameMenu.SetActive(false); });

        startMenuBackButton.onClick.RemoveAllListeners();
        startMenuBackButton.onClick.AddListener(() => { startGameMenu.SetActive(false); gameStartScreen.SetActive(true); });

        instructionsMenuBackButton.onClick.RemoveAllListeners();
        instructionsMenuBackButton.onClick.AddListener(() => { instructionsMenu.SetActive(false); optionsScreen.SetActive(true); });

        highScoreText.text = "Highest Score Achieved: " + ScoreManager.instance.GetHighScore().ToString();

        scoresMenuBackButton.onClick.RemoveAllListeners();
        scoresMenuBackButton.onClick.AddListener(() => { scoresMenu.SetActive(false); optionsScreen.SetActive(true); });

        //drugStoreButton.onClick.RemoveAllListeners();
        //drugStoreButton.onClick.AddListener(() => { GameManager.instance.SetPlayerPosition(63f, 1.35f, 45.0f); GameManager.instance.SetPlayerRotation(0.0f, 0.0f, 0.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        //stadiumButton.onClick.RemoveAllListeners();
        //stadiumButton.onClick.AddListener(() => { GameManager.instance.SetPlayerPosition(120.5f, 1.35f, 117.2f); GameManager.instance.SetPlayerRotation(0.0f, 90.0f, 0.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        //factoryButton.onClick.RemoveAllListeners();
        //factoryButton.onClick.AddListener(() => { GameManager.instance.SetPlayerPosition(195.0f, 1.35f, 63f); GameManager.instance.SetPlayerRotation(0.0f, 90.0f, 0.0f); UnityEngine.SceneManagement.SceneManager.LoadScene("City"); Time.timeScale = 1.0f; });

        //playersSpawnMenuBackButton.onClick.RemoveAllListeners();
        //playersSpawnMenuBackButton.onClick.AddListener(() => { playersSpawnMenu.SetActive(false); startGameMenu.SetActive(true); });
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        quitGamebutton.onClick.RemoveAllListeners();

        controlsButton.onClick.RemoveAllListeners();
        instructionsButton.onClick.RemoveAllListeners();
        scoresButton.onClick.RemoveAllListeners();
        optionsBackButton.onClick.RemoveAllListeners();

        controlsBackButton.onClick.RemoveAllListeners();

        easyDifficultyButton.onClick.RemoveAllListeners();
        mediumDifficultyButton.onClick.RemoveAllListeners();
        hardDifficultyButton.onClick.RemoveAllListeners();
        startMenuBackButton.onClick.RemoveAllListeners();

        instructionsMenuBackButton.onClick.RemoveAllListeners();

        scoresMenuBackButton.onClick.RemoveAllListeners();

        //drugStoreButton.onClick.RemoveAllListeners();
        //stadiumButton.onClick.RemoveAllListeners();
        //factoryButton.onClick.RemoveAllListeners();
        //playersSpawnMenuBackButton.onClick.RemoveAllListeners();
    }
}
