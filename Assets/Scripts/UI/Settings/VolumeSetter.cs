using FMODUnity;
using StarterAssets;
using UnityEngine;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] StudioGlobalParameterTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume", 0.75f);
        if (trigger != null)
        {
            trigger = GetComponent<StudioGlobalParameterTrigger>();
            trigger.Value = PlayerPrefs.GetFloat("masterVolume", 0.75f);
            trigger.TriggerParameters();
        }
    }
    private void OnEnable()
    {
        Settings.OnVolumeChanged += UpdateVolume;
    }
    private void OnDisable()
    {
        Settings.OnVolumeChanged -= UpdateVolume;
    }

    void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
        if (trigger != null)
        {
            trigger.Value = volume;
            trigger.TriggerParameters();
        }
    }
}