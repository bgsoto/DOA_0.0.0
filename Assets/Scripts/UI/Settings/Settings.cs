using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    public float _volume;
    public int _headbobOn;

    private void OnEnable()
    {
        GenericCloseMenu.CloseCurrentMenu += SaveSettings;
    }
    private void OnDisable()
    {
        GenericCloseMenu.CloseCurrentMenu -= SaveSettings;
    }
    private void Start()
    {
        _volume = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        _headbobOn = PlayerPrefs.GetInt("headbobOn", 1);
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

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}