using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using System.Collections;
using Mirror;

public class AimCameraScript : NetworkBehaviour
{
    //[SerializeField] private GameObject reticle;
    [SerializeField] private int cameraBoostAmount;
    private CinemachineVirtualCamera vcam;
    private StarterAssetsInputs input;
    private bool isAiming = false;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        vcam = GameManager.Instance.CinemachineGuraCameras[1];
        input = GetComponent<StarterAssetsInputs>();
        StartCoroutine(ChangeCamera());
    }

    IEnumerator ChangeCamera()
    {
        if(input.Aim == true && isAiming == false)
        {
            vcam.Priority += cameraBoostAmount;
            isAiming = true;
        }
        else if(input.Aim == true && isAiming == true)
        {
            yield return new WaitForSeconds(.1f);
        }
        else if(input.Aim == false && isAiming == true)
        {
            vcam.Priority -= cameraBoostAmount;
            isAiming = false;
        }

        yield return new WaitForSeconds(.1f);
        StartCoroutine(ChangeCamera());
    }
}
