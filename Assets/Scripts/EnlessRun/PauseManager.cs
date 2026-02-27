using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    [Header("Optional")]
    public GameObject joystick;

    private bool isPaused;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (joystick != null)
            joystick.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (joystick != null)
            joystick.SetActive(true);
    }
}
