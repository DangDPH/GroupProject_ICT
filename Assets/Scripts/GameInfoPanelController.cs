using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameInfoPanelController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text gameNameText;
    public Image gameIcon;
    public SceneNavigator sceneNavigator; // Link trusty SceneNavigator here!

    private CanvasGroup canvasGroup;
    private string targetSceneToLoad; // Stores the exact scene name to load

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    // Now it asks for a Name, a Picture, and a Scene!
    public void Show(string displayName, Sprite icon, string sceneName)
    {
        // 1. Update the Text
        if (gameNameText != null) gameNameText.text = displayName;

        // 2. Update the Image
        if (gameIcon != null) gameIcon.sprite = icon;

        // 3. Save the scene name so the Play button knows where to go
        targetSceneToLoad = sceneName;

        // 4. Fade it in
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // ---> WIRE PLAY BUTTON TO THIS <---
    public void PlayCurrentGame()
    {
        if (!string.IsNullOrEmpty(targetSceneToLoad))
        {
            // Use your SceneNavigator so it saves the GameID and LastPlayed data!
            if (sceneNavigator != null)
            {
                sceneNavigator.LoadScene(targetSceneToLoad);
            }
            else
            {
                Debug.LogError("SceneNavigator is missing from the GameInfoPanel!");
            }
        }
    }
}