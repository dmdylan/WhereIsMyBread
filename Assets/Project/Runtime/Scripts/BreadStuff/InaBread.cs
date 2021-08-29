using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BreadStuff
{
    public class InaBread : Bread
    {
        [SerializeField] private AbilitySO tentacleSwingAbility;
        [SerializeField] private AbilitySO takodachiAbility;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform raycastOriginPoint;
        [SerializeField] private float abilityMissDelay = 0;
        private Camera playerCamera;
        private LineRenderer lineRenderer;

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

            }
        }

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
                transform.DOMove(hit.point + moveDistance, .5f);
                var position = transform.DOMove(hit.point + moveDistance, .5f).endValue;

                StartCoroutine(HoldingTentacle(position));

                StartCoroutine(AbilityOneCooldown(tentacleSwingAbility.AbilityCooldown));
            }         
        }

        IEnumerator HoldingTentacle(Vector3 position)
        {
            if (breadInput.IsUsingAbilityOne)
            {
                transform.position = position;
            }

            if(!breadInput.IsUsingAbilityOne && !isAbilityOneReady)
            {
                yield break;
            }
            else
            {
                StartCoroutine(HoldingTentacle(position));
            }
        }
    }
}