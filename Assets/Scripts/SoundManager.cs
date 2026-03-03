using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // This creates a "Singleton" - a globally accessible reference to this exact script
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSFX;

    void Awake()
    {
        // If there is no SoundManager yet, make this the official one and make it immortal!
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If we somehow load a second one, destroy it immediately so music doesn't overlap
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Start playing the music the second the app opens
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    // Any button can call this function to play the click sound!
    public void PlayClickSound()
    {
        if (sfxSource != null && buttonClickSFX != null)
        {
            // PlayOneShot allows multiple rapid clicks to overlap naturally
            sfxSource.PlayOneShot(buttonClickSFX);
        }
    }
}