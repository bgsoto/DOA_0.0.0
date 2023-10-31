using StarterAssets;
using UnityEngine;

public class LightFollow : MonoBehaviour
{

    public Transform lightRoot;
    [SerializeField] private float speed = 2;

    private StarterAssetsInputs _input;
    [SerializeField]
    private bool playerIsMoving;

    private void Start()
    {
        _input = FindObjectOfType<StarterAssetsInputs>();
    }
    private void OnEnable()
    {
        var rotation = lightRoot.rotation;
        transform.rotation = rotation;
        transform.position = lightRoot.position;
    }

    void Update()
    {
        playerIsMoving = _input.move != Vector2.zero ? true : false;
        var rotation = lightRoot.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        transform.position = lightRoot.position;
    }
}
