using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem.XR;
using UnityEditor;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class FirstPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 4.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Speed the player moves when crouching.")]
        public float CrouchSpeed = 2.0f;
        [Tooltip("Rotation speed of the character")]
        public float RotationSpeed = 1.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        [Tooltip("Character Controller height when crouched")]
        public float CrouchHeight;
        [Tooltip("Character Controller center when crouched")]
        public Vector3 CrouchCenter;
        [Tooltip("Character Controller height when standing")]
        public float StandingHeight;
        [Tooltip("Character Controller center when standing")]
        public Vector3 StandingCenter;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.1f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Space(10)]
        [Tooltip("Speed the player head bobs when walking.")]
        public float walkBobSpeed = 14f;
        public float walkBobAmount = 0.05f;
        [Tooltip("Speed the player head bobs when sprinting.")]
        public float sprintBobSpeed = 18f;
        public float sprintBobAmount = 0.1f;
        [Tooltip("Speed the player head bobs when crouching.")]
        public float crouchBobSpeed = 8f;
        public float crouchBobAmount = 0.025f;
        private float bobTimer;

        [Space(10)]
        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.5f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;
        [Tooltip("Physics layers of obstacles that should stop the crouched player from standing")]
        public LayerMask obstacleLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;

        // cinemachine
        private float _cinemachineTargetPitch;
        private CinemachineCameraOffset _offset;

        // player
        private float _speed;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private bool isCrouching = false;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        public CinemachineVirtualCamera vcam;

        //footsteps
        public float footstepTimer = 0;
        public AudioClip[] metalSounds;
        public AudioSource source;
        [Range(0.1f, 0.5f)]
        public float volumeChangeMultiplier = 0.2f;
        [Range(0.1f, 0.5f)]
        public float pitchChangeMultiplier = 0.2f;
        [Range(0.1f, 1f)]
        public float baseVolume;
        private float baseStepSpeed = 0.5f;
        private float sprintStepMultiplier = 0.6f;
        private float crouchStepMultiplier = 1.5f;
        private float GetCurrentOffset => _input.sprint ? baseStepSpeed * sprintStepMultiplier : _input.crouch ? baseStepSpeed * crouchStepMultiplier : baseStepSpeed;
        private string GroundType;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private bool inMenu = false;

        private const float _threshold = 0.001f;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }

        private void OnEnable()
        {
            IntelMonitorManger.inIntelMenu += PlayerInput;
            ShowKeypad.DisableControls += PlayerInput;
            HubUiManager.DisablePlayerControls += PlayerInput;
            SettingsOpener.PausedGame += PlayerInput;

            /* Subscribes to event(s). */
            //UIManager.DisablePlayerControls += PlayerInput;
        }

        private void OnDisable()
        {
            IntelMonitorManger.inIntelMenu -= PlayerInput;
            ShowKeypad.DisableControls -= PlayerInput;
            HubUiManager.DisablePlayerControls -= PlayerInput;
            SettingsOpener.PausedGame -= PlayerInput;

            /* Unsubscribes from event(s). */
            //UIManager.DisablePlayerControls += PlayerInput;
        }

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            // set default cam y pos
            _offset = vcam.GetComponent<CinemachineCameraOffset>();
            RotationSpeed = PlayerPrefs.GetFloat("sensitivity", 1);
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            if (!inMenu)
            {
                JumpAndGravity();
                GroundedCheck();
                Move();
                Crouch();
                if (PlayerPrefs.GetInt("headbobOn") == 1)
                {
                    Headbob();
                }
            }
        }

        private void LateUpdate()
        {
            if (!inMenu)
            {
                CameraRotation();
                SprintFOV();
                //CheckLayers();
                Footsteps();
            }
        }

        //checks if in menu, if true, no move is processed.
        private void PlayerInput(bool value) { inMenu = value; 
            RotationSpeed = PlayerPrefs.GetFloat("sensitivity", 1); }//updates rotation speed on call too, since pausing the menu already calls this value.

        private void GroundedCheck()
        {
            // set sphere position, with offset below player
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore); //checks "Sphere position" for overlap with valid ground layers
        }

        private void CameraRotation()
        {
            // if there is an input
            if (_input.look.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

                // clamp our pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Update Cinemachine camera target pitch
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed OR crouch/crouch speed if crouching.
            float targetSpeed = _input.sprint ? SprintSpeed : isCrouching ? CrouchSpeed : MoveSpeed;
            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f; //for controllers, allows variable move magnitudes

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                // move
                inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;

            }

            // move the player
            _controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    footstepTimer = 0;
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            } //ensures player falling doesnt exceed terminal velocity
        }
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax); //clamps cam angle to 360/-360
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void SprintFOV()
        {
            vcam.m_Lens.FieldOfView = _input.sprint ? Mathf.Lerp(vcam.m_Lens.FieldOfView, 75, 10 * Time.deltaTime) : vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60, 10 * Time.deltaTime);
           /* if (_input.sprint)
            {
                if (vcam.m_Lens.FieldOfView < 50)
                {
                    vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 50, 10 * Time.deltaTime);
                }
            }
            else
            {
                if (vcam.m_Lens.FieldOfView > 40)
                {
                    vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 40, 10 * Time.deltaTime);
                }
            }*/
        }

        private void Crouch()
        {
            //sets height if crouching or not
            _controller.height = isCrouching ? 1 : 2;
            //detects crouch input

            if (_input.crouch)
            {
                _controller.height = CrouchHeight;
                _controller.center = CrouchCenter;
                isCrouching = true;
            } //checks state machine for crouch input, if true then shrinks cc height and center.

            Vector3 newBottom = StandingCenter + Vector3.down * StandingHeight * 0.5f;
            newBottom = transform.TransformPoint(newBottom); //resets bottom of cc
            if (!_input.crouch && isCrouching == true) //try to stand up if crouching but no crouch input but the player is crouching.
            {
                RaycastHit hit;
                if (Physics.SphereCast(newBottom, _controller.radius, transform.up, out hit, StandingHeight, obstacleLayers) == false)
                {
                    // Stand up only if we found no hit, otherwise try again next frame.
                    _controller.height = StandingHeight;
                    _controller.center = StandingCenter;
                    isCrouching = false;
                }
            }
        }
        private void Footsteps()
        {
            if (!_controller.isGrounded) return;
            if (_input.move == Vector2.zero) return;

            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                //if (GroundType == "Metal")
                //{
                    source.clip = metalSounds[Random.Range(0, metalSounds.Length)]; //plays footsteps with randomized pitch and volume when footstep timer is 0. timer updated every frame if moving.
                //}
                source.volume = Random.Range(baseVolume - volumeChangeMultiplier, baseVolume);
                source.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
                source.PlayOneShot(source.clip); //code from : https://www.youtube.com/watch?v=lqyzGntF5Hw //
                footstepTimer = GetCurrentOffset;
            }
        }

        private void Headbob()
        {
            float BobSpeed = isCrouching ? crouchBobSpeed : _input.sprint ? sprintBobSpeed : walkBobSpeed;
            float BobAmount = isCrouching ? crouchBobAmount : _input.sprint ? sprintBobAmount : walkBobAmount;
            if (!_controller.isGrounded) return;

            if (Mathf.Abs(new Vector2(_controller.velocity.x, _controller.velocity.z).magnitude) > 0.1f)
            {
                bobTimer += Time.deltaTime * (BobSpeed);
                _offset.m_Offset = new Vector3(
                    0f, Mathf.Sin(bobTimer) * BobAmount, 0f
                );
            }
            else if (_offset.m_Offset.y != 0f)
            {
                _offset.m_Offset = new Vector3(
                   0f, 0f, 0f
               );
            }

        }

        /*public void CheckLayers()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 3))
            {
                GroundType = hit.transform.tag;
            }
        } actually not necessary for this game, spaceships are like 100% metal lol*/
    }
}