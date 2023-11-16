using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    public float _volume;
    public int _headbobOn;
    public float _sensitivity;

    private void OnEnable()
    {
        SettingsOpener.PausedGame += SaveSettings;
    }
    private void OnDisable()
    {
        SettingsOpener.PausedGame -= SaveSettings;
    }
    private void Start()
    {
        _volume = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        _headbobOn = PlayerPrefs.GetInt("headbobOn", 1);
        _sensitivity = PlayerPrefs.GetFloat("sensitivity", 1);
    }

    public void ToggleHeadBob(bool toggle)
    {
        _headbobOn = toggle ? 1 : 0;
        PlayerPrefs.SetInt("headbobOn", _headbobOn);
    }
    public void ToggleVolume(float volume)
    {
        _volume = volume;
        PlayerPrefs.SetFloat("masterVolume", _volume);
    }
    public void ToggleSensitivity(float sensitivity)
    {
        _sensitivity = sensitivity;
        PlayerPrefs.SetFloat("sensitivity", _sensitivity);
    }

    public void SaveSettings(bool value)
    {
        PlayerPrefs.Save();
    }
}