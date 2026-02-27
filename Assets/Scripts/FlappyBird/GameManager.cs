using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;

    public TMP_Text scoreText;

    public GameObject playButton;

    public GameObject gameOver;

    private int score;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        // Try immediate auto-assign (Inspector value preferred).
        TryAssignPlayer();

        // Pause immediately (safe: Pause checks for null).
        Pause();

        // Keep trying to find a Player for a short time in case it's created at runtime.
        StartCoroutine(EnsurePlayerExists(5f));
    }

    private void TryAssignPlayer()
    {
        if (player == null)
        {
            // Look for active or inactive Player instances to be more robust.
            player = UnityEngine.Object.FindFirstObjectByType<Player>(FindObjectsInactive.Include);
        }
    }

    private IEnumerator EnsurePlayerExists(float timeout)
    {
        float elapsed = 0f;
        while (player == null && elapsed < timeout)
        {
            TryAssignPlayer();
            if (player != null) yield break;
            yield return null;
            elapsed += Time.unscaledDeltaTime;
        }

        if (player == null)
        {
            Debug.LogWarning("GameManager: Player reference not set in Inspector and no Player found in scene after waiting. Assign Player in the Inspector or ensure it's created before GameManager runs.");
        }
    }

    public void Play()
    {
         score = 0;
         scoreText.text = score.ToString();

         playButton.SetActive(false);
         gameOver.SetActive(false);

         Time.timeScale = 1f;

         if (player != null)
         {
             player.enabled = false;
             player.enabled = true;
         }

         Pipes[] pipes = Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);

         for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
         }
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        // Defensive check to avoid NullReferenceException
        if (player != null)
        {
            player.enabled = false;
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
