using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AimCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject reticle;
    private PlayerInput playerInput;
    private InputAction aimAction;
    private int priorityBoostAmount = 10;
    private CinemachineVirtualCameraBase vcam;
    private bool boosted = false;

    private void OnEnable()
    {
        playerInput.actions["Aim"].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions["Aim"].Disable();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        vcam = GameManager.Instance.CinemachineVirtualCameras[1];
        aimAction = playerInput.actions["Aim"];
        aimAction.started += AimAction_started;
        aimAction.canceled += AimAction_canceled;
    }

    private void AimAction_canceled(InputAction.CallbackContext context)
    {
        if (boosted)
        {         
            vcam.Priority -= priorityBoostAmount;
            boosted = false;
        }
        if (reticle != null)
            reticle.SetActive(boosted);
    }

    private void AimAction_started(InputAction.CallbackContext context)
    {
        if (!boosted)
        {
            vcam.Priority += priorityBoostAmount;
            boosted = true;
        }
        if (reticle != null)
            reticle.SetActive(boosted);
    }
}
