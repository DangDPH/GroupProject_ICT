using UnityEngine;
using TMPro;

public class PingPongManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text player1ScoreText; // Bottom Player
    public TMP_Text player2ScoreText; // Top Player / AI

    [Header("Screens")]
    public GameObject modeSelectionPanel;
    public GameObject gameOverPanel;
    public TMP_Text gameOverWinnerText;

    [Header("Game Elements")]
    public PlayerPaddle topPaddle;    // Drag the TopPaddle object here
    public BallController ball;       // Drag the Ball object here

    [Header("Game Settings")]
    public int scoreToWin = 5;

    private int player1Score = 0;
    private int player2Score = 0;

    private void Start()
    {
        // Show mode selection at the very start
        if (modeSelectionPanel != null) modeSelectionPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Hide the ball until a mode is selected
        if (ball != null) ball.gameObject.SetActive(false);
    }

    // --- BUTTON METHODS ---

    public void StartPvP()
    {
        // Tell the top paddle it is controlled by a human
        if (topPaddle != null) topPaddle.isAI = false;
        ResetAndStartGame();
    }

    public void StartPvBot()
    {
        // Tell the top paddle it is controlled by the computer
        if (topPaddle != null) topPaddle.isAI = true;
        ResetAndStartGame();
    }

    public void ResetAndStartGame()
    {
        // Clear logic
        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();

        // Switch Screens
        if (modeSelectionPanel != null) modeSelectionPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Turn the ball on and launch it!
        if (ball != null)
        {
            ball.gameObject.SetActive(true);
            ball.LaunchBall();
        }
    }

    // --- GAME LOGIC ---

    public void Player1Scored()
    {
        player1Score++;
        UpdateScoreUI();

        if (CheckWinAndHandleGameOver()) return;

        if (ball != null) ball.LaunchBall(); // Reset ball if game isn't over
    }

    public void Player2Scored()
    {
        player2Score++;
        UpdateScoreUI();

        if (CheckWinAndHandleGameOver()) return;

        if (ball != null) ball.LaunchBall(); // Reset ball if game isn't over
    }

    private void UpdateScoreUI()
    {
        if (player1ScoreText != null) player1ScoreText.text = player1Score.ToString();
        if (player2ScoreText != null) player2ScoreText.text = player2Score.ToString();
    }

    private bool CheckWinAndHandleGameOver()
    {
        if (player1Score >= scoreToWin || player2Score >= scoreToWin)
        {
            // Game is over! Hide the ball.
            if (ball != null) ball.gameObject.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(true);

            if (gameOverWinnerText != null)
            {
                if (player1Score >= scoreToWin)
                {
                    gameOverWinnerText.text = "PLAYER 1 WINS!";
                    gameOverWinnerText.color = Color.green;
                }
                else
                {
                    // Check if it was PvP or PvBot to show the right text
                    gameOverWinnerText.text = topPaddle.isAI ? "BOT WINS!" : "PLAYER 2 WINS!";
                    gameOverWinnerText.color = Color.red;
                }
            }
            return true; // Someone won
        }
        return false; // No one won yet
    }
}