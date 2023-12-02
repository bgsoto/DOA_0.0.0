using DG.Tweening;
using UnityEngine;

public class PlanetEECodes : MonoBehaviour
{
    [SerializeField] private AudioSource planetSource;
    [SerializeField] private Light planetLight;
    [SerializeField] private AudioClip devilClip;
    [SerializeField] private ParticleSystem devilSmoke;
    [SerializeField] private AudioClip zombiesClip;
    [SerializeField] private AudioClip sunClip;
    [SerializeField] private GameObject gnomeObject;
    [SerializeField] private AudioClip gnomeClip;
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
        if (value == "666") { DevilEgg(); } //the devil went down to curie
        if (value == "115" || value == "935") { ZombiesEgg(); } //doctor?
        if (value == "111") { SunEgg(); } //praise the sun
        if (value == "361") { SpinEgg(); } //no 0, so 361 it is
        if (value == "847") { GnomeEgg(); } //the time that half life 1 starts
    }
    void DevilEgg()
    {
        planetLight.color = Color.red;
        planetSource.PlayOneShot(devilClip);
        devilSmoke.Play();
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
    void GnomeEgg()
    {
        planetSource.PlayOneShot(gnomeClip);
        gnomeObject.SetActive(true);
        gnomeObject.transform.DOLocalMoveZ(0f, 1);
    }
}