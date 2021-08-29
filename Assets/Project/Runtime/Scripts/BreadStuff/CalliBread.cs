using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public class CalliBread : Bread
    {
        [SerializeField] private AbilitySO summonDeadbeatsAbility;
        [SerializeField] private AbilitySO secondAbility;
        [SerializeField] private GameObject deadbeatsPrefab;

        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;

            StartCoroutine(AbilityOneCooldown(summonDeadbeatsAbility.AbilityDelayTimer));
            StartCoroutine(AbilityTwoCooldown(secondAbility.AbilityDelayTimer));
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) return;

            if (isAbilityOneReady == true && breadInput.IsUsingAbilityOne == true)
            {
                SummonDeadbeatsAbility();
            }

            if (isAbilityTwoReady == true && breadInput.IsUsingAbilityTwo == true)
            {

            }
        }

        private void SummonDeadbeatsAbility()
        {
            CmdSummonDeadbeats();
            StartCoroutine(AbilityOneCooldown(summonDeadbeatsAbility.AbilityCooldown));
        }

        [Command]
        private void CmdSummonDeadbeats()
        {
            GameObject newDeadbeats = Instantiate(deadbeatsPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(newDeadbeats);
        }
    }
}