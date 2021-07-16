using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


//TODO: Need to set vcamera follow through script?
public class PlayerLocomotion : NetworkBehaviour
{
    Camera playerCamera;
    Rigidbody playerBody;
    Animator animator;
    [SerializeField] GameObject playerModel;
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction jumpAction;
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;
    private bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    private float turnSpeedMultiplier;
    private Vector3 targetDirection;
    private Quaternion freeRotation;

    private void OnEnable()
    {
        playerInput.actions["Movement"].Enable();
        playerInput.actions["Jump"].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions["Movement"].Disable();
        playerInput.actions["Jump"].Disable();
    }

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
    }

    void Start()
    {
        movementAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        movementAction.started += Movement_performed;
        movementAction.performed += Movement_performed;
        movementAction.canceled += Movement_performed;
        jumpAction.performed += JumpAction_performed;
    }

    //TODO: Pretty sure it jitters when jumping because currentMovement.y is set to 0 each frame
    //Need to read and cache before jumping
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        currentMovement = new Vector3(currentMovementInput.x, 0, currentMovementInput.y);
        currentMovement = currentMovement.x * playerCamera.transform.right.normalized + currentMovement.z * playerCamera.transform.forward.normalized;
        currentMovement.y = 0;
        animator.SetFloat("XInput", currentMovementInput.x);
        animator.SetFloat("YInput", currentMovementInput.y);
        animator.SetBool("isJumping", !isGrounded);
    }

    void FixedUpdate()
    {
        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        playerBody.MovePosition(transform.position + currentMovement * Time.deltaTime * m_Speed);
        TargetDirection();
        if (currentMovementInput != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), rotationSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
    }

    private void TargetDirection()
    {
        turnSpeedMultiplier = 1f;
        var forward = playerCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        //get the right-facing direction of the referenceTransform
        var right = playerCamera.transform.TransformDirection(Vector3.right);

        // determine the direction the player will face based on input and the referenceTransform's right and forward directions
        targetDirection = currentMovementInput.x * right + currentMovementInput.y * forward;
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void JumpAction_performed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            playerBody.AddRelativeForce(transform.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            //animator.ResetTrigger("Jump");
        }
    }
}
