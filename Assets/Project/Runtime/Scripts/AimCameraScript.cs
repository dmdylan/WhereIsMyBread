using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;

public class AimCameraScript : MonoBehaviour
{
    //[SerializeField] private GameObject reticle;
    private CinemachineVirtualCamera vcam;
    private StarterAssetsInputs input;

    void Start()
    {
        vcam = GameManager.Instance.CinemachineVirtualCameras[1];
        input = GetComponent<StarterAssetsInputs>();
    }

    private void OnAim()
    {
        vcam.Priority += 10;
    }
}
