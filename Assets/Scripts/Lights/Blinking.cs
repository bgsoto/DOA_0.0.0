using UnityEngine;

public class Blinking : MonoBehaviour
{

    public Light lightOB, lightOB1;

    public AudioSource lightSound;

    public float minTime;
    public float maxTime;
    public float timer;

    public Material greyMat;
    public Material whiteMat;

    public MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        meshRenderer.material = whiteMat;
    }

    // Update is called once per frame
    void Update()
    {
        LightsFlickering();

    }

    void LightsFlickering()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 10f)
        {
            if (lightOB != null) { lightOB.enabled = lightOB.enabled; }
            if (lightOB1 != null) { lightOB1.enabled = lightOB1.enabled; }
            meshRenderer.material = whiteMat;
            timer = Random.Range(minTime, maxTime);

        }
        if (timer >= 5f)
        {
            if (lightOB != null) { lightOB.enabled = !lightOB.enabled; }
            if (lightOB1 != null) { lightOB1.enabled = !lightOB1.enabled; }
            meshRenderer.material = greyMat;

            //lightSound.Play();
        }
    }

}
