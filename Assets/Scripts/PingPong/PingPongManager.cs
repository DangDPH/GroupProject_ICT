using UnityEngine;
using TMPro;

public class PingPongManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    [Header("Screens")]
    public GameObject modeSelectionPanel;
    public GameObject gameOverPanel;
    public TMP_Text gameOverWinnerText;

    [Header("Game Elements")]
    public PlayerPaddle topPaddle;
    public BallController ball;

    [Header("Game Settings")]
    public int scoreToWin = 5;

    private int player1Score = 0;
    private int player2Score = 0;

    private void Start()
    {
        if (modeSelectionPanel != null) modeSelectionPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (ball != null) ball.gameObject.SetActive(false);
    }

    public void StartPvP()
    {
        if (topPaddle != null) topPaddle.isAI = false;
        ResetAndStartGame();
    }

    public void StartPvBot()
    {
        if (topPaddle != null) topPaddle.isAI = true;
        ResetAndStartGame();
    }

    public void ResetAndStartGame()
    {
        Time.timeScale = 1f;
        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();

        if (modeSelectionPanel != null) modeSelectionPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (ball != null)
        {
            ball.gameObject.SetActive(true);
            ball.LaunchBall();
        }
    }

    public void Player1Scored()
    {
        player1Score++;
        UpdateScoreUI();
        if (CheckWinAndHandleGameOver()) return;
        if (ball != null) ball.LaunchBall();
    }

    public void Player2Scored()
    {
        player2Score++;
        UpdateScoreUI();
        if (CheckWinAndHandleGameOver()) return;
        if (ball != null) ball.LaunchBall();
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
                    gameOverWinnerText.text = topPaddle.isAI ? "BOT WINS!" : "PLAYER 2 WINS!";
                    gameOverWinnerText.color = Color.red;
                }
            }
            return true;
        }
        return false;
    }

    public void ReplayGame()
    {
        if (topPaddle != null)
        {
            topPaddle.transform.position = new Vector3(0, topPaddle.transform.position.y, topPaddle.transform.position.z);
        }

        ResetAndStartGame();
    }
}