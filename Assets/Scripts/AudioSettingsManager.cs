using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    // PlayerPrefs Keys matching your schema exactly
    private const string MASTER = "MasterVolume";
    private const string MUSIC = "MusicVolume";
    private const string SFX = "SFXVolume";
    private const string MUTED = "IsMuted";

    private void Start()
    {
        LoadVolumes();
    }

    // These take 'float' so Unity UI Sliders can trigger them, 
    // but we immediately convert them to Int (0-7) to match your schema.
    public void SetMasterVolume(float sliderValue)
    {
        int intVol = Mathf.Clamp(Mathf.RoundToInt(sliderValue), 0, 7);
        PlayerPrefs.SetInt(MASTER, intVol);
        UpdateMixer(MASTER, intVol);
    }

    public void SetMusicVolume(float sliderValue)
    {
        int intVol = Mathf.Clamp(Mathf.RoundToInt(sliderValue), 0, 7);
        PlayerPrefs.SetInt(MUSIC, intVol);
        UpdateMixer(MUSIC, intVol);
    }

    public void SetSFXVolume(float sliderValue)
    {
        int intVol = Mathf.Clamp(Mathf.RoundToInt(sliderValue), 0, 7);
        PlayerPrefs.SetInt(SFX, intVol);
        UpdateMixer(SFX, intVol);
    }

    // New method to handle the IsMuted boolean from your schema
    public void SetMuted(bool isMuted)
    {
        PlayerPrefs.SetInt(MUTED, isMuted ? 1 : 0);
        LoadVolumes(); // Reloading applies the mute state instantly
    }

    private void LoadVolumes()
    {
        // 1 = True, 0 = False. Defaulting to 0 (unmuted).
        bool isMuted = PlayerPrefs.GetInt(MUTED, 0) == 1;

        if (isMuted)
        {
            // If muted, drop all audio mixer parameters to -80dB
            audioMixer.SetFloat(MASTER, -80f);
            audioMixer.SetFloat(MUSIC, -80f);
            audioMixer.SetFloat(SFX, -80f);
        }
        else
        {
            // Default to 7 (max volume) if no save data exists yet
            UpdateMixer(MASTER, PlayerPrefs.GetInt(MASTER, 7));
            UpdateMixer(MUSIC, PlayerPrefs.GetInt(MUSIC, 7));
            UpdateMixer(SFX, PlayerPrefs.GetInt(SFX, 7));
        }
    }

    private void UpdateMixer(string parameterName, int vol0to7)
    {
        // Convert the 0-7 integer back into a 0.0f - 1.0f percentage for the math
        float linearValue = vol0to7 / 7f;
        audioMixer.SetFloat(parameterName, ToDB(linearValue));
    }

    float ToDB(float value)
    {
        return value <= 0.001f ? -80f : Mathf.Log10(value) * 20f;
    }
}