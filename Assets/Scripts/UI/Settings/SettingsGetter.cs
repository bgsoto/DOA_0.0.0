using UnityEngine;
using UnityEngine.UI;

public class SettingsGetter : MonoBehaviour
{
    [SerializeField] private Toggle headbobToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider sensitivitySlider;

    private void OnEnable()
    {
        headbobToggle.isOn = PlayerPrefs.GetInt("headbobOn") == 1 ? true : false;
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }
}