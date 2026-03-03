using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // Needed to control the Sliders

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    // These must EXACTLY match the names in your Exposed Parameters list!
    private const string MIXER_MASTER = "MasterVol";
    private const string MIXER_MUSIC = "MusicVol";
    private const string MIXER_SFX = "SFXVol";

    // PlayerPrefs Keys for saving data
    private const string PREF_MASTER = "MasterVolume";
    private const string PREF_MUSIC = "MusicVolume";
    private const string PREF_SFX = "SFXVolume";
    private const string PREF_MUTED = "IsMuted";

    [Header("UI References")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // 1. Load the 0-7 values from memory (default to 7 if new)
        int mVol = PlayerPrefs.GetInt(PREF_MASTER, 7);
        int muVol = PlayerPrefs.GetInt(PREF_MUSIC, 7);
        int sVol = PlayerPrefs.GetInt(PREF_SFX, 7);
        bool isMuted = PlayerPrefs.GetInt(PREF_MUTED, 0) == 1;

        // 2. Update the Sliders visually
        if (masterSlider != null) masterSlider.value = mVol;
        if (musicSlider != null) musicSlider.value = muVol;
        if (sfxSlider != null) sfxSlider.value = sVol;
        if (muteToggle != null) muteToggle.isOn = isMuted;

        // 3. Apply to the Mixer
        ApplyAllSettings(isMuted, mVol, muVol, sVol);
    }

    public void SetMasterVolume(float value) => SaveAndApply(PREF_MASTER, MIXER_MASTER, value);
    public void SetMusicVolume(float value) => SaveAndApply(PREF_MUSIC, MIXER_MUSIC, value);
    public void SetSFXVolume(float value) => SaveAndApply(PREF_SFX, MIXER_SFX, value);

    public void SetMuted(bool isMuted)
    {
        PlayerPrefs.SetInt(PREF_MUTED, isMuted ? 1 : 0);
        LoadSettings(); // Refresh everything
    }

    private void SaveAndApply(string prefKey, string mixerKey, float sliderValue)
    {
        int intVol = Mathf.RoundToInt(sliderValue);
        PlayerPrefs.SetInt(prefKey, intVol);

        // Only update mixer if not currently muted
        if (PlayerPrefs.GetInt(PREF_MUTED, 0) == 0)
        {
            UpdateMixer(mixerKey, intVol);
        }
    }

    private void ApplyAllSettings(bool muted, int m, int mu, int s)
    {
        if (muted)
        {
            audioMixer.SetFloat(MIXER_MASTER, -80f);
            audioMixer.SetFloat(MIXER_MUSIC, -80f);
            audioMixer.SetFloat(MIXER_SFX, -80f);
        }
        else
        {
            UpdateMixer(MIXER_MASTER, m);
            UpdateMixer(MIXER_MUSIC, mu);
            UpdateMixer(MIXER_SFX, s);
        }
    }

    private void UpdateMixer(string parameterName, int vol0to7)
    {
        float linearValue = vol0to7 / 7f; // Convert 0-7 scale to 0-1
        float dbValue = linearValue <= 0.001f ? -80f : Mathf.Log10(linearValue) * 20f;
        audioMixer.SetFloat(parameterName, dbValue);
    }
}