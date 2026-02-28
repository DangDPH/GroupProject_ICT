using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("UI Controls")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    private bool isMuted = false;

    void Start()
    {
        // 1. Load saved preferences (Default to level 7, and Mute = false)
        float savedMaster = PlayerPrefs.GetFloat("MasterLevel", 7f);
        float savedMusic = PlayerPrefs.GetFloat("MusicLevel", 7f);
        float savedSFX = PlayerPrefs.GetFloat("SFXLevel", 7f);
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1; // 1 means true, 0 means false

        // 2. Update UI visuals to match saved data
        masterSlider.value = savedMaster;
        musicSlider.value = savedMusic;
        sfxSlider.value = savedSFX;
        muteToggle.isOn = isMuted;

        // 3. Apply initial audio levels
        ApplyAllVolumes();

        // 4. Listen for UI changes
        masterSlider.onValueChanged.AddListener(delegate { SaveAndApply(); });
        musicSlider.onValueChanged.AddListener(delegate { SaveAndApply(); });
        sfxSlider.onValueChanged.AddListener(delegate { SaveAndApply(); });
        muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    public void ToggleMute(bool muted)
    {
        isMuted = muted;
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        ApplyAllVolumes();
    }

    public void SaveAndApply()
    {
        // Save current slider positions
        PlayerPrefs.SetFloat("MasterLevel", masterSlider.value);
        PlayerPrefs.SetFloat("MusicLevel", musicSlider.value);
        PlayerPrefs.SetFloat("SFXLevel", sfxSlider.value);

        ApplyAllVolumes();
    }

    private void ApplyAllVolumes()
    {
        // If global mute is on, override the Master channel to complete silence
        if (isMuted)
        {
            mainMixer.SetFloat("MasterVol", -80f);
        }
        else
        {
            CalculateAndSet("MasterVol", masterSlider.value);
        }

        // Music and SFX are always calculated, but they are subject to the Master Mixer group
        CalculateAndSet("MusicVol", musicSlider.value);
        CalculateAndSet("SFXVol", sfxSlider.value);
    }

    private void CalculateAndSet(string paramName, float level)
    {
        // If the slider is at 0, mute that specific channel
        if (level <= 0f)
        {
            mainMixer.SetFloat(paramName, -80f);
        }
        else
        {
            // Convert level (1-7) to percentage, then to logarithmic decibels
            float percentage = level / 7f;
            mainMixer.SetFloat(paramName, Mathf.Log10(percentage) * 20f);
        }
    }
}