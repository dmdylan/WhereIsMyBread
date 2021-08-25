using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BreadStuff
{
    public class BreadMovementController : NetworkBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private GameObject groundCheck;
        [Range(0.0f, 0.3f)]
        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private float jumpForce = 1f;
        [SerializeField] private GameObject wallCheck;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float wallCheckSphereRadius = .2f;
        [SerializeField] private float wallCheckSphereDistance = .1f;

        [Header("Camera")]
        [SerializeField] private GameObject cameraFollowTarget;
        [SerializeField] private float cameraMoveSpeed = 1f;
        [SerializeField] private float topClamp = 70.0f;
        [SerializeField] private float bottomClamp = -30.0f;
        [SerializeField] private float cameraAngleOverride = 0.0f;

        [Header("Ground Checks")]
        [SerializeField] private float groundCheckRadius = 1f;
        [SerializeField] private LayerMask groundLayers;

        //Animation stuff
        private Animator animator;
        private int runningID;
        private int jumpingID;
        private int groundedID;

        //Camera and movement
        private Camera playerCamera;
        private const float threshold = 0.01f;
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;
        private Vector3 targetDirection;
        private float targetRotation;
        private float _rotationVelocity;
        private bool isGrounded;

        //Misc
        private Rigidbody playerRigidBody;
        private BreadInput breadInput;
        private Bread bread;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            playerRigidBody = GetComponent<Rigidbody>();
            playerCamera = Camera.main;
            breadInput = GetComponent<BreadInput>();
            bread = GetComponent<Bread>();
            animator = GetComponent<Animator>();
            SetBreadCameras();
            SetAnimatorHash();
            StartCoroutine(GroundCheck());
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            Move();
            Jump();
        }

        private void LateUpdate()
        {
            if (!isLocalPlayer) return;

            CameraRotation();
        }

        private void Move()
        {
            breadInput.MoveInput.Normalize();

            if (breadInput.MoveInput == Vector2.zero)
            {
                animator.SetBool(runningID, false);
                return;
            }

            if (breadInput.MoveInput != Vector2.zero)
            {
                //Get the angle of input based on the camera direction
                targetRotation = Mathf.Atan2(breadInput.MoveInput.x, breadInput.MoveInput.y) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, rotationSmoothTime);

                // rotate to face input direction relative to camera position
                playerRigidBody.MoveRotation(Quaternion.Euler(0.0f, rotation, 0.0f));
            }

            //Forward movement direction based on which direction the body is facing
            targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            //Check if there is a wall right in front of us
            if (Physics.SphereCast(wallCheck.transform.position, wallCheckSphereRadius, targetDirection, out _, wallCheckSphereDistance, layerMask))
                return;

            playerRigidBody.MovePosition(playerRigidBody.position + (targetDirection * moveSpeed * Time.deltaTime));
            animator.SetBool(runningID, true);
        }

        private void Jump()
        {
            if (breadInput.IsJumping == true && isGrounded == true)
            {
                playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetTrigger(jumpingID);
                bread.SetState(BreadState.Falling);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (breadInput.LookInput.sqrMagnitude >= threshold)
            {
                cinemachineTargetYaw += breadInput.LookInput.x * cameraMoveSpeed * Time.deltaTime;
                cinemachineTargetPitch += breadInput.LookInput.y * cameraMoveSpeed  * Time.deltaTime;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            // Cinemachine will follow this target
            cameraFollowTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }

        private void SetAnimatorHash()
        {
            runningID = Animator.StringToHash("isRunning");
            jumpingID = Animator.StringToHash("jumped");
            groundedID = Animator.StringToHash("isGrounded");
        }

        private IEnumerator GroundCheck()
        {
            isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayers, QueryTriggerInteraction.Ignore);

            if (isGrounded == true && bread.BreadState != BreadState.Grounded) 
                bread.SetState(BreadState.Grounded);

            animator.SetBool(groundedID, isGrounded);

            yield return new WaitForSeconds(0);
            StartCoroutine(GroundCheck());
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }

        private void SetBreadCameras()
        {
            GameManager.Instance.CinemachineVirtualCameras[2].Follow = cameraFollowTarget.transform;
            GameManager.Instance.CinemachineVirtualCameras[2].LookAt = cameraFollowTarget.transform;
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
        }
    }
}