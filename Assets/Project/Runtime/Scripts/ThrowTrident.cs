using Mirror;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrident : NetworkBehaviour
{
    [SerializeField] private GameObject tridentPrefab;
    [SerializeField] private Transform tridentSpawnPosition;
    [SerializeField] private float throwTimer = 2f;
    private bool canThrow = true;
    private StarterAssetsInputs input;
    private Camera playerCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        input = GetComponent<StarterAssetsInputs>();
        playerCamera = Camera.main;
    }

    void OnThrow()
    {
        if (!isLocalPlayer) return;

        if (canThrow == false || input.Aim == false) return;

        // Create a ray from the camera going through the middle of your screen
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000); // You may need to change this value according to your needs

        tridentSpawnPosition.LookAt(targetPoint);

        StartCoroutine(ThrowTimer());
        CmdSpawnTrident();
    }

    [Command]
    private void CmdSpawnTrident()
    {
        GameObject newTrident = Instantiate(tridentPrefab, tridentSpawnPosition.position, tridentSpawnPosition.rotation);
        NetworkServer.Spawn(newTrident);
    }

    IEnumerator ThrowTimer()
    {
        canThrow = false;
        yield return new WaitForSeconds(throwTimer);
        canThrow = true;
    }
}
