using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfiniteTicTacToe : MonoBehaviour
{
    [Header("UI References")]
    public Button[] cells;
    public TMP_Text[] cellTexts;
    public TMP_Text turnIndicatorText;

    [Header("Screens")]
    public GameObject modeSelectionPanel;
    public GameObject gameOverPanel;
    public TMP_Text gameOverWinnerText;

    [Header("Aesthetics")]
    public Color colorX = Color.red;
    public Color colorO = Color.blue;
    public float markFontSize = 150f;

    private int[] boardState = new int[9];
    private Queue<int> playerXQueue = new Queue<int>();
    private Queue<int> playerOQueue = new Queue<int>();

    private bool isPlayerXTurn = true;
    private bool isGameOver = false;
    private bool isBotMode = false;

    private void Start()
    {
        foreach (TMP_Text txt in cellTexts)
        {
            if (txt != null) txt.fontSize = markFontSize;
        }

        // Show mode selection at the very start
        modeSelectionPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    // --- BUTTON METHODS ---

    public void StartPvP()
    {
        isBotMode = false;
        ResetAndStartGame();
    }

    public void StartPvBot()
    {
        isBotMode = true;
        ResetAndStartGame();
    }

    public void ResetAndStartGame()
    {
        // Clear logic
        boardState = new int[9];
        playerXQueue.Clear();
        playerOQueue.Clear();
        isGameOver = false;
        isPlayerXTurn = true;

        // Clear UI
        for (int i = 0; i < 9; i++)
        {
            cellTexts[i].text = "";
        }

        modeSelectionPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        UpdateTurnIndicator();
    }

    // --- GAME LOGIC ---

    public void OnCellClicked(int cellIndex)
    {
        if (isGameOver || boardState[cellIndex] != 0) return;

        // If it's the Bot's turn, ignore human clicks
        if (isBotMode && !isPlayerXTurn) return;

        // Human places mark (X always, or O if PvP)
        if (isPlayerXTurn)
        {
            PlaceMark(cellIndex, 1, "X", playerXQueue, colorX);
        }
        else
        {
            PlaceMark(cellIndex, 2, "O", playerOQueue, colorO);
        }

        if (CheckWinAndHandleGameOver()) return;

        SwitchTurns();

        // Trigger Bot if needed
        if (isBotMode && !isPlayerXTurn && !isGameOver)
        {
            // Small delay so the bot doesn't move instantly
            Invoke(nameof(PlayBotMove), 0.5f);
        }
    }

    private void PlayBotMove()
    {
        if (isGameOver) return;

        // Find all empty cells
        List<int> emptyCells = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (boardState[i] == 0) emptyCells.Add(i);
        }

        // Pick a random empty cell
        if (emptyCells.Count > 0)
        {
            int botChoice = emptyCells[Random.Range(0, emptyCells.Count)];
            PlaceMark(botChoice, 2, "O", playerOQueue, colorO);

            if (CheckWinAndHandleGameOver()) return;

            SwitchTurns();
        }
    }

    private void PlaceMark(int cellIndex, int playerID, string symbol, Queue<int> playerQueue, Color markColor)
    {
        boardState[cellIndex] = playerID;
        cellTexts[cellIndex].text = symbol;
        cellTexts[cellIndex].color = markColor;
        playerQueue.Enqueue(cellIndex);

        if (playerQueue.Count > 3)
        {
            int oldestCellIndex = playerQueue.Dequeue();
            boardState[oldestCellIndex] = 0;
            cellTexts[oldestCellIndex].text = "";
        }
    }

    private void SwitchTurns()
    {
        isPlayerXTurn = !isPlayerXTurn;
        UpdateTurnIndicator();
    }

    private void UpdateTurnIndicator()
    {
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = isPlayerXTurn ? "X's Turn" : "O's Turn";
            turnIndicatorText.color = isPlayerXTurn ? colorX : colorO;
        }
    }

    private bool CheckWinAndHandleGameOver()
    {
        if (CheckWin())
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);

            if (gameOverWinnerText != null)
            {
                gameOverWinnerText.text = (isPlayerXTurn ? "X" : "O") + " WINS!";
                gameOverWinnerText.color = isPlayerXTurn ? colorX : colorO;
            }
            return true;
        }
        return false;
    }

    private bool CheckWin()
    {
        int[,] winPatterns = new int[,] {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
            {0, 4, 8}, {2, 4, 6}             // Diagonals
        };

        for (int i = 0; i < 8; i++)
        {
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];

            if (boardState[a] != 0 && boardState[a] == boardState[b] && boardState[a] == boardState[c])
            {
                return true;
            }
        }
        return false;
    }
}