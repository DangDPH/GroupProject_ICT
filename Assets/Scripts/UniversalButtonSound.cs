using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UniversalButtonSound : MonoBehaviour
{
    private Button myButton;

    void Start()
    {
        // Automatically grab the Button component on this object
        myButton = GetComponent<Button>();

        // Automatically tell the button to play the sound when clicked!
        myButton.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        // Reach out into the void, find the immortal SoundManager, and play the SFX!
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}