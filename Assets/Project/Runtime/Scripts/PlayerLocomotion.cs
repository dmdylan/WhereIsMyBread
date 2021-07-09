using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerLocomotion : NetworkBehaviour
{
    Rigidbody playerBody;
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private PlayerInput playerInput;
    private InputAction movementAction;
    private InputAction jumpAction;
    private Vector3 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;
    private bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;

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

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void FixedUpdate()
    {
        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        playerBody.MovePosition(transform.position + currentMovement * Time.deltaTime * m_Speed);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void JumpAction_performed(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            playerBody.AddRelativeForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
