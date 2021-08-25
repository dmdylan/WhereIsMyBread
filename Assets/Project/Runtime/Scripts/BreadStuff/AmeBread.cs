using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public class AmeBread : Bread
    {
        public AbilitySO rewindTimeAbility;
        public AbilitySO watsonConcoctionAbility;

        private Queue<Vector3> previousPositions;

        private void Update()
        {
            Debug.Log("Using ability one: " + breadInput.IsUsingAbilityOne);
            Debug.Log("Using ability two: " + breadInput.IsUsingAbilityTwo);
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

        [Command]
        private void CmdSetPosition()
        {
            transform.position = previousPositions.Peek();
        }
    }
}