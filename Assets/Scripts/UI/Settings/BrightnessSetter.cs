using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class BrightnessSetter : MonoBehaviour
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
        profile.GetSetting<ColorGrading>().brightness.value = brightness;
    }
    void ChangeGamma(float gamma)
    {
        profile.GetSetting<ColorGrading>().gamma.value = new Vector4(1f, 1f, 1f, gamma);
    }
}