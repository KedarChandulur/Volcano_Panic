using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    UIManager uI_Manager;

    public static event EventHandler GameoverEvent;

    [SerializeField]
    private float gameTimeInSeconds = 120f;
    private float currentTime;
    [SerializeField]
    private uint totalHostageCount = 0;
    private uint hostagesLeft = 0;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = gameTimeInSeconds;

        if(!GameObject.FindGameObjectWithTag("UIManager").TryGetComponent<UIManager>(out uI_Manager))
        {
            Debug.LogError("UI Manager not set");
            return;
        }

        uI_Manager.InitTotalTime(gameTimeInSeconds);
        uI_Manager.InitTotalHostagesCount(totalHostageCount);

        hostagesLeft = totalHostageCount;

        UIManager.OnRescueButtonClickedEvent += UIManager_OnRescueButtonClickedEvent;
        RescueEventHandler.OnReachingDestination += RescueEventHandler_OnReachingDestination;
    }

    private void OnDestroy()
    {
        UIManager.OnRescueButtonClickedEvent -= UIManager_OnRescueButtonClickedEvent;
        RescueEventHandler.OnReachingDestination -= RescueEventHandler_OnReachingDestination;
    }

    private void RescueEventHandler_OnReachingDestination(object sender, System.EventArgs e)
    {
        uI_Manager.UpdateScore(hostagesLeft);
    }

    private void UIManager_OnRescueButtonClickedEvent(object sender, uint e)
    {
        this.DecrementHostageCount(e);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
    }

    public void UpdateTotalHostageCount_Init(uint hostageCount)
    {
        totalHostageCount += hostageCount;
    }

    public void DecrementHostageCount(uint hostageCount)
    {
        hostagesLeft -= hostageCount;
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
        Time.timeScale = 0f;

        GameoverEvent?.Invoke(this, EventArgs.Empty);

        SceneManager.LoadScene("EndGameScreen");
    }
}
