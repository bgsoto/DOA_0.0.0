using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 60.0f; // Set your desired rotation speed in degrees per second

    void Update()
    {
        // Calculate the rotation angle based on input and speed
        float rotationAngle =  rotationSpeed * Time.deltaTime;

        // Rotate the camera around its up axis (Y-axis)
        transform.Rotate(Vector3.up, rotationAngle);
    }
}