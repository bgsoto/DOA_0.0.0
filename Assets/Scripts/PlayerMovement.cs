using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rb;

    public float speed;
    public float horizontalInput;
    public float verticalInput;
    
     public Transform Orientation;
     public Vector3 moveDirection;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;


    }

    private void Update()
    {
        myInput();
    }

    private void FixedUpdate()
    {
        playerMove();
    }

    private void myInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


    }
    private void playerMove()
    {

        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
        
        rb.AddForce(moveDirection.normalized * speed * 10f , ForceMode.Force);
    }



}
