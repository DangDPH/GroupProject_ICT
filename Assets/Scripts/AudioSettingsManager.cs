using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private const string MASTER = "MasterVolume";
    private const string MUSIC = "MusicVolume";
    private const string SFX = "SFXVolume";

    private void Start()
    {
        LoadVolumes();
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(MASTER, ToDB(value));
        PlayerPrefs.SetFloat(MASTER, value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MUSIC, ToDB(value));
        PlayerPrefs.SetFloat(MUSIC, value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(SFX, ToDB(value));
        PlayerPrefs.SetFloat(SFX, value);
    }

    private void LoadVolumes()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX, 1f));
    }

    float ToDB(float value)
    {
        return value <= 0.001f ? -80f : Mathf.Log10(value) * 20f;
    }
}
