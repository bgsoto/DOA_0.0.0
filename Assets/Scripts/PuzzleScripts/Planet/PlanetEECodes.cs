using DG.Tweening;
using UnityEngine;

public class PlanetEECodes : MonoBehaviour
{
    [SerializeField] private AudioSource planetSource;
    [SerializeField] private Light planetLight;
    [SerializeField] private AudioClip devilClip;
    [SerializeField] private ParticleSystem greenSmoke;
    [SerializeField] private AudioClip zombiesClip;
    [SerializeField] private AudioClip sunClip;
    private void OnEnable()
    {
        PlanetsManager.planetWrong += CodeChecker;
    }
    private void OnDisable()
    {
        PlanetsManager.planetWrong -= CodeChecker;
    }
    void CodeChecker(string value)
    {
        if (value == "666") { DevilEgg(); }
        if (value == "421") { GreenEgg(); }
        if (value == "115") { ZombiesEgg(); }
        if (value == "111") { SunEgg(); }
        if (value == "361") { SpinEgg(); }
    }
    void DevilEgg()
    {
        planetLight.color = Color.red;
        planetSource.PlayOneShot(devilClip);
    }
    void GreenEgg()
    {
        planetLight.color = Color.green;
        greenSmoke.Play();
    }
    void ZombiesEgg()
    {
        planetLight.color = Color.blue;
        planetSource.PlayOneShot(zombiesClip);
    }
    void SunEgg()
    {
        planetLight.color = Color.yellow;
        planetSource.PlayOneShot(sunClip);
    }
    void SpinEgg()
    {
        gameObject.transform.DOLocalRotate(new Vector3(0, 360, 0), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
    }
}
