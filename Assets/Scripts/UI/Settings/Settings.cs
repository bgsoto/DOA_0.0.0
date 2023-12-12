using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    public float _volume;
    public float _headbobOn;
    public float _sensitivity;
    public float _brightness;
    public float _gamma;
    
    //subscriptions: BrightnessSetter
    public static Action<float> OnBrightnessChanged;
    public static Action<float> OnGammaChanged;
    public static Action<float> OnVolumeChanged;
    private void OnEnable()
    {
        SettingsOpener.PausedGame += SaveSettings;
    }
    private void OnDisable()
    {
        SettingsOpener.PausedGame -= SaveSettings;
    }
    private void Awake()
    {
        InitializeSettings();
        SaveSettings(true);
    }

    public void ToggleHeadBob(bool toggle)
    {
        _headbobOn = toggle ? 1 : 0;
        PlayerPrefs.SetFloat("headbobOn", _headbobOn);
    }
    public void ToggleVolume(float volume)
    {
        _volume = volume;
        PlayerPrefs.SetFloat("masterVolume", _volume);
        OnVolumeChanged?.Invoke(_volume);
    }
    public void ToggleSensitivity(float sensitivity)
    {
        _sensitivity = sensitivity;
        PlayerPrefs.SetFloat("sensitivity", _sensitivity);
    }
    public void ToggleBrightness(float brightness)
    {
        _brightness = brightness;
        PlayerPrefs.SetFloat("brightness", _brightness);
        OnBrightnessChanged?.Invoke(_brightness);
    }
    public void ToggleGamma(float gamma)
    {
        _gamma = gamma;
        PlayerPrefs.SetFloat("gamma", _gamma);
        OnGammaChanged?.Invoke(_gamma);
    }
    public void SaveSettings(bool value)
    {
        PlayerPrefs.Save();
    }
    void InitializeSettings()
    {
        _volume = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        _headbobOn = PlayerPrefs.GetFloat("headbobOn", 1f);
        _sensitivity = PlayerPrefs.GetFloat("sensitivity", 1f);
        _brightness = PlayerPrefs.GetFloat("brightness", 0f);
        _gamma = PlayerPrefs.GetFloat("gamma", 0f);
    }
}