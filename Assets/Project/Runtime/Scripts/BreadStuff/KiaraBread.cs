using DG.Tweening;
using DG;
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
        private bool canRevive = true;
        private bool inFlight = false;

        private void Start()
        {
            if (!isLocalPlayer) return;

            DOTween.Init();
            playerRigidbody = GetComponent<Rigidbody>();
            StartCoroutine(AbilityOneCooldown(flightAbility.AbilityDelayTimer));
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            if (isAbilityOneReady == true && breadInput.IsUsingAbilityOne == true)
            {
                FlightAbility();
            }
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
            inFlight = true;
        }

        //TODO: Rotate ridgidbody with dotween, add thrust, rotate towards camera facing direction
        IEnumerator StartFlight()
        {
            inFlight = true;
            isAbilityOneReady = false;
            playerRigidbody.useGravity = false;
            
            yield return new WaitForSeconds(flightAbility.AbilityDuration);
            inFlight = false;
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