using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gameModePanel;
    public GameObject settingsPanel;

    void Start()
    {
        ShowMainMenu();  // Show main menu on start
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowGameModeMenu()
    {
        mainMenuPanel.SetActive(false);
        gameModePanel.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
