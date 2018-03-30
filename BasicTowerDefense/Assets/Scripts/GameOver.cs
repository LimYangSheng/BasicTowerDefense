using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static GameOver gameOverUI;

    //Variables to set up GameOver UI
    public GameObject ui;
    public Text numWaves;

    private bool hasShownFinalWaveCount;

    // Variable to handle game restart
    public Button restartButton;

    void Awake()
    {
        gameOverUI = this;
        hasShownFinalWaveCount = false;
    }

    public void DisplayGameOverUI()
    {
        // Display the game over UI
        ui.SetActive(true);
        // Wave count should only be updated once
        if (!hasShownFinalWaveCount)
        {
            SetNumberOfWaves();
            hasShownFinalWaveCount = true;
        }
    }

    // Set the game over UI number of waves survived text
    public void SetNumberOfWaves()
    {
        numWaves.text = WaveSpawner.GetCurrentWave().ToString();
    }

    // Return if the game is over
    public bool isGameOver()
    {
        // Returns only true when game is over
        return hasShownFinalWaveCount;
    }

    // Restarts the Game
    public void RestartGame()
    {
        hasShownFinalWaveCount = false;
        WaveSpawner.ResetCurrentWave();
        SceneManager.LoadScene("MainScene");
    }
}
