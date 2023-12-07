using UnityEngine;
using UnityEngine.UI;

public class SettingsGetter : MonoBehaviour
{
    [SerializeField] private Toggle headbobToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider gammaSlider;

    private void OnEnable()
    {
        headbobToggle.isOn = PlayerPrefs.GetInt("headbobOn") == 1 ? true : false;
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
        gammaSlider.value = PlayerPrefs.GetFloat("gamma");
    }
}