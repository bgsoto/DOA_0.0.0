using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("CameraPosition Object from Player")]
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
