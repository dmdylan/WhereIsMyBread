using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public class CalliBread : Bread
    {
        [SerializeField] private GameObject deadbeatsPrefab;

        // Start is called before the first frame update
        void Start()
        {
            if (!isLocalPlayer) return;

            StartCoroutine(AbilityOneCooldown(abilityOneSO.AbilityDelayTimer));
            StartCoroutine(AbilityTwoCooldown(abilityTwoSO.AbilityDelayTimer));
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
            StartCoroutine(AbilityOneCooldown(abilityOneSO.AbilityCooldown));
        }

        [Command]
        private void CmdSummonDeadbeats()
        {
            GameObject newDeadbeats = Instantiate(deadbeatsPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(newDeadbeats);
        }
    }
}