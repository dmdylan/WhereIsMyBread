using Mirror;
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
        [SerializeField] private float groundDrag = 5f;
        [SerializeField] private float airDrag = 0f;
        [SerializeField] private float downForce = 1f;

        [Header("Camera")]
        [SerializeField] private GameObject cameraFollowTarget;
        [SerializeField] private float topClamp = 70.0f;
        [SerializeField] private float bottomClamp = -30.0f;
        [SerializeField] private float cameraAngleOverride = 0.0f;

        [Header("Ground Checks")]
        [SerializeField] private float groundCheckRadius = 1f;
        [SerializeField] private LayerMask groundLayers;

        private Rigidbody playerRigidBody;
        private Camera playerCamera;
        private BreadInput breadInput;
        private Bread bread;
        private Vector3 targetDirection;
        private const float threshold = 0.01f;
        private float cinemachineTargetYaw;
        private float cinemachineTargetPitch;
        private float targetRotation;
        private float _rotationVelocity;
        private bool isGrounded;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            playerRigidBody = GetComponent<Rigidbody>();
            SetBreadCameras();
            playerCamera = Camera.main;
            breadInput = GetComponent<BreadInput>();
            bread = GetComponent<Bread>();
            StartCoroutine(GroundCheck());
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            Jump();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            Move();

            if(bread.BreadState == BreadState.Falling)
            {
                playerRigidBody.AddForce(Vector3.down * downForce, ForceMode.VelocityChange);
            }
        }

        //_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime* SpeedChangeRate);

        private void LateUpdate()
        {
            if (!isLocalPlayer) return;

            CameraRotation();
        }

        private void Move()
        {
            breadInput.MoveInput.Normalize();

            if (breadInput.MoveInput != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(breadInput.MoveInput.x, breadInput.MoveInput.y) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, rotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            if (breadInput.MoveInput == Vector2.zero) return;

            targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

            //TODO: Might be better to set velocity instead?
            playerRigidBody.AddForce(targetDirection * moveSpeed, ForceMode.VelocityChange);
        }

        private void Jump()
        {
            if (breadInput.IsJumping == true && isGrounded == true)
            {
                playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                bread.SetState(BreadState.Falling);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (breadInput.LookInput.sqrMagnitude >= threshold)
            {
                cinemachineTargetYaw += breadInput.LookInput.x * Time.deltaTime;
                cinemachineTargetPitch += breadInput.LookInput.y * Time.deltaTime;
            }

            // clamp our rotations so our values are limited 360 degrees
            cinemachineTargetYaw = ClampAngle(cinemachineTargetYaw, float.MinValue, float.MaxValue);
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

            // Cinemachine will follow this target
            cameraFollowTarget.transform.rotation = Quaternion.Euler(cinemachineTargetPitch + cameraAngleOverride, cinemachineTargetYaw, 0.0f);
        }

        private IEnumerator GroundCheck()
        {
            isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayers, QueryTriggerInteraction.Ignore);

            if (isGrounded == true && bread.BreadState != BreadState.Grounded)
                bread.SetState(BreadState.Grounded);

            yield return new WaitForSeconds(0);
            StartCoroutine(GroundCheck());
        }

        IEnumerator SetDrag()
        {
            playerRigidBody.drag = airDrag;
            yield return new WaitUntil(() => bread.BreadState == BreadState.Grounded);
            playerRigidBody.drag = groundDrag;
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