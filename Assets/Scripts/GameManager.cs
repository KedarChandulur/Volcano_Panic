using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Scene currentActiveScene;

    public static GameManager instance;

    UIManager uiManager;

    public static event EventHandler GameEndEvent;

    private float gameTimeInSeconds = 120f;
    private float currentTime;

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

            uiManager.InitTotalTime(gameTimeInSeconds);

            isGameOver = false;
        }
    }

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

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                uiManager.FlipPauseFunctionality();
            }
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0.0f;
        isGameOver = true;
        GameEndEvent?.Invoke(this, EventArgs.Empty);
        SceneManager.LoadScene("EndGameScreen");
    }

    public void SetGameTime(float _totalTime)
    {
        this.gameTimeInSeconds = _totalTime;
    }

    //public Vector3 GetPlayerPosition()
    //{
    //    return this.playerPosition;
    //}

    //public void SetPlayerPosition(float x, float y, float z)
    //{
    //    this.playerPosition.x = x;
    //    this.playerPosition.y = y;
    //    this.playerPosition.z = z;
    //}

    //public Vector3 GetPlayerRotation()
    //{
    //    return this.playerRotation;
    //}

    //public void SetPlayerRotation(float x, float y, float z)
    //{
    //    this.playerRotation.x = x;
    //    this.playerRotation.y = y;
    //    this.playerRotation.z = z;
    //}
}
