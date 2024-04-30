using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene currentActiveScene;

    public static GameManager instance;

    UIManager uI_Manager;

    private float gameTimeInSeconds = 120f;
    private float currentTime;

    [SerializeField]
    private uint totalHostageCount = 0;
    private uint hostagesSaved = 0;

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
        Debug.Log(changedTo.name + " Scene Loaded.");

        currentActiveScene = changedTo;

        if (currentActiveScene.buildIndex == 1) // Doing index comparisions as its faster than strings
        {
            if (!GameObject.FindGameObjectWithTag("UIManager").TryGetComponent<UIManager>(out uI_Manager))
            {
                Debug.LogError("UI Manager not set");
                return;
            }

            currentTime = gameTimeInSeconds;
            hostagesSaved = 0;

            uI_Manager.InitTotalTime(gameTimeInSeconds);
            uI_Manager.InitTotalHostagesCount(totalHostageCount);

            isGameOver = false;
        }
    }

    public void UponReachingDestination()
    {
        uI_Manager.UpdateScore(hostagesSaved);
    }

    public void IncreaseHostageSaveCount(uint hostageCount)
    {
        hostagesSaved += hostageCount;
    }

    private void Update()
    {
        if (currentActiveScene.buildIndex == 1 && !isGameOver) // Doing index comparisions as its faster than strings
        {
            UpdateTimer();
        }
    }

    public void UpdateTotalHostageCount_Init(uint hostageCount)
    {
        totalHostageCount += hostageCount;
    }

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            EndGame();
        }

        uI_Manager.UpdateTimerDisplay(currentTime);
    }

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
