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

    [Header("Aesthetics")]
    public Color colorX = Color.red;    // X defaults to Red
    public Color colorO = Color.blue;   // O defaults to Blue
    public float markFontSize = 150f;   // Adjust this in the Inspector to fit your grid

    // 0 = empty, 1 = Player X, 2 = Player O
    private int[] boardState = new int[9];

    // Queues to enforce the 3-mark rolling constraint
    private Queue<int> playerXQueue = new Queue<int>();
    private Queue<int> playerOQueue = new Queue<int>();

    private bool isPlayerXTurn = true;
    private bool isGameOver = false;

    private void Start()
    {
        // Automatically make the font bigger for all cells when the game starts
        foreach (TMP_Text txt in cellTexts)
        {
            if (txt != null)
            {
                txt.fontSize = markFontSize;
            }
        }
    }

    public void OnCellClicked(int cellIndex)
    {
        // Ignore click if game is over or cell is already taken
        if (isGameOver || boardState[cellIndex] != 0) return;

        if (isPlayerXTurn)
        {
            PlaceMark(cellIndex, 1, "X", playerXQueue, colorX);
        }
        else
        {
            PlaceMark(cellIndex, 2, "O", playerOQueue, colorO);
        }

        if (CheckWin())
        {
            isGameOver = true;

            // Safety check added here to prevent the crash!
            if (turnIndicatorText != null)
            {
                turnIndicatorText.text = (isPlayerXTurn ? "X" : "O") + " WINS!";
                turnIndicatorText.color = isPlayerXTurn ? colorX : colorO; // Matches text color to winner
            }
            return;
        }

        // Switch turns
        isPlayerXTurn = !isPlayerXTurn;
        if (turnIndicatorText != null)
        {
            turnIndicatorText.text = (isPlayerXTurn ? "X's Turn" : "O's Turn");
            turnIndicatorText.color = isPlayerXTurn ? colorX : colorO; // Matches text color to current turn
        }
    }

    private void PlaceMark(int cellIndex, int playerID, string symbol, Queue<int> playerQueue, Color markColor)
    {
        // 1. Place the new mark
        boardState[cellIndex] = playerID;
        cellTexts[cellIndex].text = symbol;
        cellTexts[cellIndex].color = markColor; // Apply the Red or Blue color
        playerQueue.Enqueue(cellIndex);

        // 2. Enforce the Rolling Constraint (Max 3 marks)
        if (playerQueue.Count > 3)
        {
            int oldestCellIndex = playerQueue.Dequeue();
            boardState[oldestCellIndex] = 0; // Clear the logical state
            cellTexts[oldestCellIndex].text = ""; // Clear the UI
        }
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

            // If the cells are not empty and all match the same player ID
            if (boardState[a] != 0 &&
                boardState[a] == boardState[b] &&
                boardState[a] == boardState[c])
            {
                return true;
            }
        }
        return false;
    }
}