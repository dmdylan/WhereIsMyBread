using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateOnKeypress : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction aimAction;
    public int PriorityBoostAmount = 10;
    public GameObject Reticle;

    Cinemachine.CinemachineVirtualCameraBase vcam;
    bool boosted = false;

    private void OnEnable()
    {
        playerInput.actions["Aim"].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions["Aim"].Disable();
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        vcam = GetComponent<Cinemachine.CinemachineVirtualCameraBase>();
        aimAction.started += AimAction_started;
        aimAction.canceled += AimAction_canceled;
    }

    private void AimAction_canceled(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void AimAction_started(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (vcam != null)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (!boosted)
                {
                    vcam.Priority += PriorityBoostAmount;
                    boosted = true;
                }
            }
            else if (boosted)
            {
                vcam.Priority -= PriorityBoostAmount;
                boosted = false;
            }
        }
        if (Reticle != null)
            Reticle.SetActive(boosted);
    }
}
