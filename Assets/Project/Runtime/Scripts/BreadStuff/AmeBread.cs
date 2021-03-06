using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public class AmeBread : Bread
    {
        private Queue<Vector3> previousPositions;

        private void Start()
        {
            if (!isLocalPlayer) return;

            previousPositions = new Queue<Vector3>();

            StartCoroutine(AddPositionsToPositionQueue());
            StartCoroutine(AbilityOneCooldown(abilityOneSO.AbilityDelayTimer));
            StartCoroutine(AbilityTwoCooldown(abilityTwoSO.AbilityDelayTimer));
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            if (isAbilityOneReady == true && breadInput.IsUsingAbilityOne == true)
            {
                RewindTimeAbility();
            }

            if (isAbilityTwoReady == true && breadInput.IsUsingAbilityTwo == true)
            {
                WatsonConcoctionAbility();
            }                     
        }

        private void RewindTimeAbility()
        {
            CmdSetPosition();
            StartCoroutine(AbilityOneCooldown(abilityOneSO.AbilityCooldown));
        }

        private void WatsonConcoctionAbility()
        {
            CmdSetMoveSpeed();
            StartCoroutine(AbilityTwoCooldown(abilityTwoSO.AbilityCooldown));
        }

        private IEnumerator AddPositionsToPositionQueue()
        {
            if (previousPositions.Count < 40)
                previousPositions.Enqueue(transform.position);
            else
            {
                previousPositions.Dequeue();
                previousPositions.Enqueue(transform.position);
            }

            yield return new WaitForSeconds(.25f);

            StartCoroutine(AddPositionsToPositionQueue());
        }

        private IEnumerator ChangeMoveSpeed(float moveSpeedMultiplier, float duration)
        {
            float baseSpeed = breadMovementController.MoveSpeed;
            breadMovementController.MoveSpeed *= moveSpeedMultiplier;

            yield return new WaitForSeconds(duration);

            breadMovementController.MoveSpeed = baseSpeed;
        }

        [Command]
        private void CmdSetPosition()
        {
            transform.position = previousPositions.Peek();
        }

        [Command]
        private void CmdSetMoveSpeed()
        {
            StartCoroutine(ChangeMoveSpeed(abilityTwoSO.AbilityEffectFloat, abilityTwoSO.AbilityDuration));
        }
    }
}