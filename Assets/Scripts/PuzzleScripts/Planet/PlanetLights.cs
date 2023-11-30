using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLights : MonoBehaviour
{
    [SerializeField]
    private List<Light> currentLights = new List<Light>();
    private int nextLight = 0;
    private void OnEnable()
    {
        PlanetInteract.UpdateInput += UpdateLights;
        PlanetsManager.planetWrong += WrongLights;
        PlanetsManager.planetCorrect += CorrectLights;
    }
    private void OnDisable()
    {
        PlanetInteract.UpdateInput -= UpdateLights;
        PlanetsManager.planetWrong -= WrongLights;
        PlanetsManager.planetCorrect -= CorrectLights;
    }

    void UpdateLights(int i)
    {
        currentLights[nextLight].enabled = true;
        nextLight++;
        if (nextLight >= currentLights.Count)
        {
            nextLight = 0;
        }
    }
    void CorrectLights()
    {
        foreach (Light light in currentLights)
        {
            light.DOColor(Color.green, 0.4f).OnComplete(() =>
            {
                light.DOColor(Color.white, 0.2f).OnComplete(() =>
                {
                    light.DOColor(Color.green, 0.4f).OnComplete(() =>
                    {
                        light.DOColor(Color.white, 0.2f).OnComplete(() =>
                        { light.DOColor(Color.green, 0.4f); });
                    });
                });
            });
        }
    }
    void WrongLights(string value)
    {
        foreach (Light light in currentLights)
        {
            light.DOColor(Color.white, 1f).OnComplete(() =>
            {
                light.DOColor(Color.red, 0.2f).OnComplete(() =>
                {
                    light.DOColor(Color.white, 0.4f).OnComplete(() =>
                    {
                        light.DOColor(Color.red, 0.2f).OnComplete(() =>
                        {
                            light.DOColor(Color.white,0);
                            light.enabled = false;}); });
                });
            });
        }
    }
}