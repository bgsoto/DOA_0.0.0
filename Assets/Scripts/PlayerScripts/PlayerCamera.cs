using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

  public float SensitivityY; // Y sensitivity
  public float SensitivityX; // X sensitivity

   public Transform Orientation; // player orientation

     public float Xrotation; // X rotation
     public float Yrotation; // Y rotation


    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

    }

    private void Update()
    {
        // GET MOUSE INPUT

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;


        Yrotation += mouseX;
        Xrotation -= mouseY;

        Xrotation = Mathf.Clamp (Xrotation, -90f, 90f);

        // Rotate Orientation

        transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);

        Orientation.rotation = Quaternion.Euler(0, Yrotation, 0);
    
        
    
    
    }













}
