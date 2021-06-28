using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MLAPI;

public class PlayerMovement : NetworkBehaviour
{
    PlayerControls playerControls;
    Rigidbody playerBody;
    Vector2 currentMovementInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeedMultiplier = 1f;
    private bool isMovementPressed;
    Vector3 currentMovement;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Drag")]
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }


    private void OnEnable()
    {
        playerControls.PlayerActions.Enable();
    }

    private void OnDisable()
    {
        playerControls.PlayerActions.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerBody = GetComponent<Rigidbody>();

        playerControls.PlayerActions.Movement.started += OnMovementInput;
        playerControls.PlayerActions.Movement.canceled += OnMovementInput;
        playerControls.PlayerActions.Movement.performed += OnMovementInput;
        playerControls.PlayerActions.Jump.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerBody.AddRelativeForce(currentMovement * moveSpeedMultiplier, ForceMode.VelocityChange);
        HandleRotation();
        ControlPlayerDrag();
    }    
    
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        Debug.Log(currentMovementInput);
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);
            playerBody.AddRelativeForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            playerBody.MoveRotation(targetRotation);
        }
    }

    private void ControlPlayerDrag()
    {
        if (isGrounded)
        {
            playerBody.drag = groundDrag;
        }
        else
        {
            playerBody.drag = airDrag;
        }
    }
}
