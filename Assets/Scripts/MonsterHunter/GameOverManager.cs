using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame()
    {
        // Reset time
        Time.timeScale = 1f;
        
        // Load curent scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
