using DG.Tweening;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public class KiaraBread : Bread
    {
        [SerializeField] private AbilitySO flightAbility;
        [SerializeField] private AbilitySO reviveAbility;

        private Rigidbody playerRigidbody;
        private Camera playerCamera;
        private bool canRevive = true;
        private bool inFlight = false;

        private void Start()
        {
            if (!isLocalPlayer) return;

            DOTween.Init();
            playerCamera = Camera.main;
            playerRigidbody = GetComponent<Rigidbody>();
            StartCoroutine(AbilityOneCooldown(flightAbility.AbilityDelayTimer));
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            if (isAbilityOneReady == true && breadInput.IsUsingAbilityOne == true)
            {
                //FlightAbility();
                StartCoroutine(StartFlight());
            }
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer) return;

            if (inFlight)
                FlightAbility();
        }

        public override void Destroyed()
        {
            if(canRevive == true)
            {
                ReviveAbility();
            }
            else
            {
                base.Destroyed();
            }
        }

        #region Flight Ability

        private void FlightAbility()
        {
            playerRigidbody.AddForce(playerCamera.transform.TransformDirection(Vector3.forward) * flightAbility.AbilityEffectFloat, 
                ForceMode.VelocityChange);

            playerRigidbody.MoveRotation(Quaternion.Euler(playerCamera.transform.TransformDirection(Vector3.forward)));
        }

        //TODO: Rotate ridgidbody with dotween, add thrust, rotate towards camera facing direction
        IEnumerator StartFlight()
        {
            inFlight = true;
            isAbilityOneReady = false;
            playerRigidbody.useGravity = false;
            yield return new WaitForSeconds(flightAbility.AbilityDuration);
            inFlight = false;
            playerRigidbody.useGravity = true;
            isAbilityOneReady = true;
        }

        #endregion

        #region Revive Ability

        private void ReviveAbility()
        {
            StartCoroutine(ReviveAbilityCooldown());
            CmdSetHealth();
            CmdRandomRespawn();
        }

        IEnumerator ReviveAbilityCooldown()
        {
            canRevive = false;
            yield return new WaitForSeconds(reviveAbility.AbilityCooldown);
            canRevive = true;
        }

        #endregion

        #region Commands

        [Command]
        private void CmdSetHealth()
        {
            health = maxHealth;
        }

        [Command]
        private void CmdRandomRespawn()
        {
            int count = NetworkManager.startPositions.Count;
            transform.position = NetworkManager.startPositions[Random.Range(0, count)].position;
        }

        #endregion
    }
}