using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Game Identity")]
    public string gameDisplayName; // What shows up on the panel (e.g., "Ping Pong")
    public string targetSceneName; // The exact file name (e.g., "Game_PingPong")
    public Sprite gameIcon;        // The picture of the game

    [Header("Panel Reference")]
    public GameInfoPanelController infoPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (infoPanel != null)
        {
            // Send this card's specific data to the big panel!
            infoPanel.Show(gameDisplayName, gameIcon, targetSceneName);
        }
        else
        {
            Debug.LogError("Game Info Panel is not linked to this card!");
        }
    }
}