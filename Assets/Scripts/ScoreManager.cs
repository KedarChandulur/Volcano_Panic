using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    Scene currentActiveScene;

    public static ScoreManager instance;

    UIManager uiManager;

    private uint highScoreAchieved = 0;
    private uint possibleScore = 0;
    private uint currentScore = 0;

    private uint totalHostageCount = 0;
    private uint hostagesSaved = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InstantiateScoreManager()
    {
        if (instance == null)
        {
            GameObject scoreManagerObject = new GameObject("ScoreManager");
            scoreManagerObject.AddComponent<ScoreManager>();
            scoreManagerObject.tag = "ScoreController";
            DontDestroyOnLoad(scoreManagerObject);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        GameManager.GameEndEvent += GameManager_GameEndEvent;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
        GameManager.GameEndEvent -= GameManager_GameEndEvent;
    }

    private void SceneManager_activeSceneChanged(Scene changedFrom, Scene changedTo)
    {
        currentActiveScene = changedTo;

        if (currentActiveScene.buildIndex == 1) // Doing index comparisions as its faster than strings
        {
            if (!GameObject.FindGameObjectWithTag("UIManager").TryGetComponent<UIManager>(out uiManager))
            {
                Debug.LogError("UI Manager not set");
                return;
            }

            hostagesSaved = 0;

            uiManager.InitTotalHostagesCount(totalHostageCount);
            UpdateCurrentScore();
        }
    }

    private void GameManager_GameEndEvent(object sender, System.EventArgs e)
    {
        if(currentScore > highScoreAchieved)
            highScoreAchieved = currentScore;
    }

    public void UpdateTotalHostageCount_Init(uint hostageCount)
    {
        totalHostageCount += hostageCount;
    }

    public void IncreaseHostageSaveCount(uint hostageCount)
    {
        hostagesSaved += hostageCount;
    }

    public void IncrementPossibleScore(uint incrementValue)
    {
        possibleScore += incrementValue;
    }

    public void UponHostagesSaved_UI()
    {
        uiManager.UpdateHostagesSaved(hostagesSaved);
    }

    public void UpdateCurrentScore()
    {
        currentScore = possibleScore;
        uiManager.UpdateScore(currentScore);
    }

    public uint GetHighScore()
    {
        return highScoreAchieved;
    }
}
