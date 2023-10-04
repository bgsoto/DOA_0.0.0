using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Orientation Object from Player")]
    [SerializeField] private Transform orientation;

    [Header("Camera Settings")]
    [Range(500, 1500)]
    [SerializeField] private float sensitivityX;
    [Range(500, 1500)]
    [SerializeField] private float sensitivityY;

    private bool inMenu = false;
    private float rotationX;
    private float rotationY;

    private void OnEnable()
    {
        /* Subscribes to event(s). */
        UIManager.DisablePlayerControls += PlayerInput;
    }

    private void OnDisable()
    {
        /* Unsubscribes from event(s). */
        UIManager.DisablePlayerControls += PlayerInput;
    }

    private void Update()
    {
        if (!inMenu)
        {
            Camera();
        }
    }

    /* Pauses player input when a menu is displayed */
    private void PlayerInput(bool value) { inMenu = value; }

    private void Camera()
    {
        /* Retrieve input from mouse movement. */
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivityY;

        /* 
         * Appies left and right movement of the mouse. 
         * When looking left and right, an object rotates on the Y-AXIS. 
         */
        rotationY += mouseX;

        /* 
         * Applies forward and backward movement of the mouse. 
         * When looking left and right, an object rotates on the X-AXIS. 
         * 
         * If mouseY was added to rotationX, the up and down rotation of the camera 
         * would be inverted. Subtracting mouseY corresponds to the actual movement 
         * of the mouse. 
         */
        rotationX -= mouseY;

        /* Restricts the camera's rotation to 90 degress on the X-AXIS. */
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        /* Applies values to rotate camera. */
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
