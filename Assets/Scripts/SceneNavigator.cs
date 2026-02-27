using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneNavigator : MonoBehaviour
{
    private static string previousScene;

    public void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // ONLY for Settings / shared back logic
    public void GoBackFromSetting()
    {
        SceneManager.LoadScene(
            string.IsNullOrEmpty(previousScene)
            ? "OpenMenu"
            : previousScene
        );
    }

    // UI-safe wrappers
    public void GoToOpenMenu() => LoadScene("OpenMenu");
    public void GoToGameHub() => LoadScene("MainGameHubMenu");
    public void GoToSettings() => LoadScene("SettingMenu");
    public void GoToCredits() => LoadScene("CreditShows");
}
