using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string currentLevel;
    private Dictionary<string, bool> levelsCompleted = new Dictionary<string, bool>();
    public GameObject settingsPanel;
        
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Initialize game state (load from file or set default)
        currentLevel = "MainMenu";  // Starting point
        levelsCompleted["Level1"] = false;
        levelsCompleted["Level2"] = false;
        levelsCompleted["SecretLevel"] = false;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");  // Load your main game scene
    }
    //Paneling
    public void QuitGame()
    {
        Application.Quit();  // Close the application (only works in a built game)
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");  // Load settings scene
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);  // Hide the settings panel
    }

    public void OpenGameModeSelect()
    {
        settingsPanel.SetActive(true);  // Show the game mode panel
    }

    public void CloseGameModeSelect()
    {
        settingsPanel.SetActive(false);  // Hide the game mode panel
    }
        
    // Call this when the player completes a level
    public void CompleteLevel(string levelName)
    {
        levelsCompleted[levelName] = true;
        // Based on choices, unlock other levels
        if (levelName == "Level1")
        {
            LoadLevel("Level2");  // Direct path
        }
        else if (levelName == "Level2")
        {
            LoadLevel("SecretLevel");  // Non-linear path
        }
    }

    // Function to load levels dynamically
    public void LoadLevel(string levelName)
    {
        currentLevel = levelName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }

    // Function to check if a level is completed
    public bool IsLevelCompleted(string levelName)
    {
        return levelsCompleted.ContainsKey(levelName) && levelsCompleted[levelName];
    }
}

