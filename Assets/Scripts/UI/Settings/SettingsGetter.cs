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
        headbobToggle.isOn = PlayerPrefs.GetFloat("headbobOn", 1) == 1 ? true : false;
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 1);
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
        gammaSlider.value = PlayerPrefs.GetFloat("gamma");
    }
}