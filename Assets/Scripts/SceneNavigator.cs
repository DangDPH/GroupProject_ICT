using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneNavigator : MonoBehaviour
{
    // This static variable acts as our "memory". It survives scene loads!
    private static string previousSceneName = "";

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;

        string activeScene = SceneManager.GetActiveScene().name;

        // Save game progress if we are leaving a game scene
        if (activeScene.StartsWith("Game_"))
        {
            PlayerPrefs.SetString("GameID", activeScene);
            PlayerPrefs.SetString("LastPlayed", DateTime.Now.ToString("O"));
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(sceneName);
    }

    // --- SETTINGS LOGIC ---

    // 1. Used by the Pause Menu inside your actual games
    public void GoToSettingsFromGame()
    {
        // Load settings ON TOP of the current game (creates 2 active scenes)
        SceneManager.LoadScene("SettingMenu", LoadSceneMode.Additive);
    }

    // 2. Used by the OpenMenu and MainGameHubMenu
    public void GoToSettings()
    {
        // Right before we leave, save the current scene's name into our static memory!
        previousSceneName = SceneManager.GetActiveScene().name;
        LoadScene("SettingMenu");
    }

    // 3. The universal Back button logic for the Setting Menu
    public void GoBackFromSetting()
    {
        // If more than 1 scene is loaded, we are an overlay on top of a paused game
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("SettingMenu");
        }
        else
        {
            Time.timeScale = 1f;

            // If previousSceneName is empty (meaning you hit Play while already inside the SettingMenu in the Editor)
            if (string.IsNullOrEmpty(previousSceneName))
            {
                SceneManager.LoadScene("OpenMenu");
            }
            else
            {
                // Go back to the exact scene we saved in memory (OpenMenu or MainGameHubMenu)
                SceneManager.LoadScene(previousSceneName);
            }
        }
    }
    // --------------------------

    public void GoToOpenMenu() => LoadScene("OpenMenu");
    public void GoToGameHub() => LoadScene("MainGameHubMenu");
    public void GoToCredits() => LoadScene("CreditShows");
}