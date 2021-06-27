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
    Vector3 currentMovement;

    [SerializeField] private float rotationSpeed = 10.0f;

    private bool isMovementPressed;

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
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        playerBody.velocity = currentMovement * Time.deltaTime;
    }    
    
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        Debug.Log(currentMovementInput);
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed);
        }
    }
}
