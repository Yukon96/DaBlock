using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelectManager : MonoBehaviour
{
    public string currentLevel;
    private List<string> gameModes = new List<string>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Initialize game state (load from file or set default)
        currentLevel = "MainMenu";  // Starting point
        //levelsCompleted["Level1"] = false;
        //levelsCompleted["Level2"] = false;
        //levelsCompleted["SecretLevel"] = false;
    }

    // Call this when the player completes a level
    public void CompleteLevel(string levelName)
    {
        //levelsCompleted[levelName] = true;
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
    //public bool IsLevelCompleted(string levelName)
    //{
    //    return levelsCompleted.ContainsKey(levelName) && levelsCompleted[levelName];
    //}
}

