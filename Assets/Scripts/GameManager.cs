using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene currentActiveScene;

    public static GameManager instance;

    UIManager uiManager;

    private float gameTimeInSeconds = 120f;
    private float currentTime;

    //private uint totalHostageCount = 0;
    //private uint hostagesSaved = 0;

    private bool isGameOver = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InstantiateGameManager()
    {
        if (instance == null)
        {
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
            gameManagerObject.tag = "GameController";
            DontDestroyOnLoad(gameManagerObject);
        }
    }

    private void Awake()
    {
        if(instance == null)
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
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
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

            currentTime = gameTimeInSeconds;
            //hostagesSaved = 0;

            uiManager.InitTotalTime(gameTimeInSeconds);
            //uiManager.InitTotalHostagesCount(totalHostageCount);

            isGameOver = false;
        }
    }

    //public void UponReachingDestination()
    //{
    //    uiManager.UpdateScore(hostagesSaved);
    //}

    //public void IncreaseHostageSaveCount(uint hostageCount)
    //{
    //    hostagesSaved += hostageCount;
    //}

    private void Update()
    {
        if (currentActiveScene.buildIndex == 1 && !isGameOver) // Doing index comparisions as its faster than strings
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                EndGame();
            }

            uiManager.Update_Tick(currentTime);
        }
    }

    //public void UpdateTotalHostageCount_Init(uint hostageCount)
    //{
    //    totalHostageCount += hostageCount;
    //}

    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("EndGameScreen");
    }

    public void SetGameTime(float _totalTime)
    {
        this.gameTimeInSeconds = _totalTime;
    }
}
