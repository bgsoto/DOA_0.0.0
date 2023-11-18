using UnityEngine;

public class LightFollow : MonoBehaviour
{

    public Transform lightRoot;
    public Light lightObject;
    [SerializeField] private float speed = 12;

    private void OnEnable()
    {
        var rotation = lightRoot.rotation;
        transform.rotation = rotation;
        transform.position = lightRoot.position;
        transform.SetParent(null);
    }

    void Update()
    {
        if (lightRoot.gameObject.activeInHierarchy)
        {
            lightObject.enabled = true;
            var rotation = lightRoot.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            transform.position = lightRoot.position;
        }
        else { lightObject.enabled = false; }
    }
}
