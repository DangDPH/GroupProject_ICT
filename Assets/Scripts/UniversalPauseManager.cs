using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalPauseManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseUIContainer; // <--- We will put PauseBlur here!

    void Start()
    {
        if (pauseUIContainer != null) pauseUIContainer.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseUIContainer != null) pauseUIContainer.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        // This will now hide the PauseBlur AND everything inside it!
        if (pauseUIContainer != null) pauseUIContainer.SetActive(false);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}