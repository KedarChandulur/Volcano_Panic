using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager uI_Manager;
    public float gameTimeInSeconds = 120f;
    private float currentTime;
    private bool isGameOver = false;

    private void Start()
    {
        currentTime = gameTimeInSeconds;

        if(!GameObject.FindGameObjectWithTag("UIManager").TryGetComponent<UIManager>(out uI_Manager))
        {
            Debug.LogError("UI Manager not set");
            return;
        }

        uI_Manager.UpdateTimerDisplay(currentTime);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
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
        Debug.Log("Game Over!");
    }
}
