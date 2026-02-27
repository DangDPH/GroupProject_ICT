using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Game Info")]
    public string gameId;   // "tictactoe", "pong", "snake"

    [Header("UI Reference")]
    public GameInfoPanelController gameInfoPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked game: " + gameId);

        if (gameInfoPanel != null)
        {
            gameInfoPanel.Show(gameId);
        }
        else
        {
            Debug.LogWarning("GameInfoPanelController is NOT assigned!");
        }
    }
}
