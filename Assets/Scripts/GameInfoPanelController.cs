using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameInfoPanelController : MonoBehaviour
{
    public TMP_Text gameNameText;
    public Image gameIcon;

    private CanvasGroup canvasGroup;
    private string currentGameId;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup missing on GameInfoPanel!");
            return;
        }

        Hide();
    }

    public void Show(string gameId)
    {
        currentGameId = gameId;

        if (gameNameText != null)
            gameNameText.text = gameId;

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public string GetCurrentGameId()
    {
        return currentGameId;
    }
}
