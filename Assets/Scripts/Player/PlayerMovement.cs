using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDrag;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private Transform orientation;
    private Vector3 moveDirection;
    
    private float horizontalInput;
    private float verticalInput;
    private bool inMenu = false;
    private bool isGrounded;
    private float rayLength;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = transform.Find("Orientation").transform;
        rayLength = (playerHeight * 0.5f) + 0.2f;
    }


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
            /* 
             * Shoots Raycast from the center of the object and determines if it hits an another object 
             * with the layer mask of "Ground". 
             */
            isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);

            GetInput();
            SpeedControl();

            /* 
             * Applies drag if the object is on the ground.
             * Shorthand if-statement Syntax: <value> = <condition> ? <valueIfTrue> : <valueIfFalse>
             */
            rb.drag = isGrounded ? groundDrag : 0;
        }
    }

    /* Pauses player input when a menu is displayed */
    private void PlayerInput(bool value) { inMenu = value; }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        /* Retrives input from WASD, Arrow Keys, or Joystick. */
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SpeedControl()
    {
        /* Gets current velocity of the object. */
        Vector3 currentVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        /* Sets the object's velocity to the moveSpeed if it exceeds the moveSpeed. */
        if (currentVelocity.magnitude > moveSpeed)
        {
            Vector3 maxVelocity = currentVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }

    private void MovePlayer()
    {
        /* Calculates the movement based on inputs. */
        moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        
        /* Applies force based on the direction of movement. */
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
