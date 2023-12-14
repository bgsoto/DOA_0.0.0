using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class BrightnessSetter : NetworkBehaviour
{
    [SerializeField] private PostProcessProfile profile;
    private void OnEnable()
    {
        Settings.OnBrightnessChanged += ChangeBrightness;
        Settings.OnGammaChanged += ChangeGamma;
    }
    private void OnDisable()
    {
        Settings.OnBrightnessChanged -= ChangeBrightness;
        Settings.OnGammaChanged -= ChangeGamma;
    }
    void ChangeBrightness(float brightness)
    {
        if (!IsOwner) return;
        profile.GetSetting<ColorGrading>().brightness.value = brightness;
    }
    void ChangeGamma(float gamma)
    {
        if (!IsOwner) return;
        profile.GetSetting<ColorGrading>().gamma.value = new Vector4(1f, 1f, 1f, gamma);
    }
}