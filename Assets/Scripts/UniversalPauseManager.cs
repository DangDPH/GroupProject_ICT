using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalPauseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuPanel;

    void Start()
    {
        // Hide the menu and ensure time is running normally when the scene loads
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Freezes the game!
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreezes the game
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f; // MUST unfreeze before reloading
        // Automatically finds the current scene's name and restarts it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenSettings()
    {
        Time.timeScale = 1f; // Unfreeze before leaving

        // LEAVE THE BREADCRUMB: Save the exact name of the current scene (e.g., "Game_PingPong")
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene("SettingMenu"); // Navigates to your settings scene
    }

    public void ReturnToHub()
    {
        Time.timeScale = 1f; // Unfreeze before leaving
        SceneManager.LoadScene("MainGameHubMenu"); // Navigates to your Hub scene
    }
}