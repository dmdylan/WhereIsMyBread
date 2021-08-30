using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Mirror;

namespace BreadStuff
{
    public class InaBread : Bread
    {
        [SerializeField] private AbilitySO tentacleSwingAbility;
        [SerializeField] private AbilitySO takodachiAbility;
        [SerializeField] private GameObject takodachiPrefab;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform raycastOriginPoint;
        [SerializeField] private float abilityMissDelay = 0;
        private Camera playerCamera;
        private LineRenderer lineRenderer;
        private bool isSwinging;

        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;

            DOTween.Init();
            playerCamera = Camera.main;
            lineRenderer = GetComponent<LineRenderer>();
            StartCoroutine(AbilityOneCooldown(tentacleSwingAbility.AbilityDelayTimer));
            StartCoroutine(AbilityTwoCooldown(takodachiAbility.AbilityDelayTimer));
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            if (isAbilityOneReady == true && breadInput.IsUsingAbilityOne == true)
            {
                TentacleSwing();
            }

            if (isAbilityTwoReady == true && breadInput.IsUsingAbilityTwo == true)
            {
                SummonTakodachiAbility();
            }

            if (isSwinging)
            {
                lineRenderer.SetPosition(0, transform.position);
            }
        }

        private void SummonTakodachiAbility()
        {
            CmdSummonTakodachi();
            StartCoroutine(AbilityTwoCooldown(takodachiAbility.AbilityCooldown));
        }

        [Command]
        private void CmdSummonTakodachi()
        {
            GameObject newTakodachi = Instantiate(takodachiPrefab, transform.position + transform.forward * 2, transform.rotation);
            NetworkServer.Spawn(newTakodachi);
        }

        #region Tentacle

        private void TentacleSwing()
        {
            var direction = playerCamera.transform.forward;
            Physics.Raycast(raycastOriginPoint.position, direction, out RaycastHit hit, tentacleSwingAbility.AbilityEffectFloat, layerMask);
            Debug.DrawRay(raycastOriginPoint.position, direction * tentacleSwingAbility.AbilityEffectFloat, Color.green);

            if (hit.collider == null)
            {
                StartCoroutine(AbilityOneCooldown(abilityMissDelay));
            }
            else
            {
                var moveDistance = hit.normal / 2;
                var travelPoint = hit.point + moveDistance;

                transform.DOMove(travelPoint, .5f);

                StartCoroutine(HoldingTentacle(travelPoint));
                lineRenderer.SetPosition(1, hit.point);
                StartCoroutine(AbilityOneCooldown(tentacleSwingAbility.AbilityCooldown));
            }         
        }

        IEnumerator HoldingTentacle(Vector3 position)
        {
            isSwinging = true;
            lineRenderer.enabled = true;
            yield return new WaitUntil(() => Vector3.Distance(transform.position, position) <= 1f);
            isSwinging = false;
            lineRenderer.enabled = false;
        }
        #endregion
    }
}