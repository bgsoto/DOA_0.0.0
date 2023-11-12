using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightFollow : MonoBehaviour
{

    public Transform lightRoot;
    public Light lightObject;
    [SerializeField] private float speed = 2;
    [SerializeField] private bool playerIsMoving;

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
            if (playerIsMoving)
            {
                var rotation = lightRoot.rotation;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            }
            else { transform.rotation = lightRoot.rotation; }
            transform.position = lightRoot.position;
        }
        else { lightObject.enabled = false; } 
    }

    private void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            playerIsMoving = true;
            transform.hasChanged = false;
        }
        else
        {
            playerIsMoving = false;
        }
    }
}