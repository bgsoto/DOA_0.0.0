using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using StarterAssets;

public class VolumeSetter : MonoBehaviour
{
    [SerializeField] StudioGlobalParameterTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        trigger = GetComponent<StudioGlobalParameterTrigger>();
        trigger.Value = PlayerPrefs.GetFloat("masterVolume");
    }
    private void OnEnable()
    {
        StarterAssetsInputs.pausePressed += UpdateVolume;
    }
    private void OnDisable()
    {
        StarterAssetsInputs.pausePressed -= UpdateVolume;
    }

    void UpdateVolume()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        trigger.Value = PlayerPrefs.GetFloat("masterVolume");
        trigger.TriggerParameters();
    }
}