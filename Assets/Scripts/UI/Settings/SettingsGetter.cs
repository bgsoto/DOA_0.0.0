using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsGetter : MonoBehaviour
{
    [SerializeField] private Toggle headbobToggle;
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        headbobToggle.isOn = PlayerPrefs.GetInt("headbobOn") == 1 ? true : false;
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

}
