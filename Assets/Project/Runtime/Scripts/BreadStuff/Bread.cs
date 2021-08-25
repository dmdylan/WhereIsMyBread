using Mirror;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public enum BreadState { Grounded, Falling, Dead }

    public abstract class Bread : NetworkBehaviour, IDamageable
    {
        [SyncVar]
        [SerializeField] protected int health;

        [SerializeField] private int maxHealth = 2;
        //[SerializeField] private float damageSpeed = 8f;
        //[SerializeField] private float damagedSpeedTimeBeforeDecay = 2f;
        //[SerializeField] private float damagedSpeedDecayTime = 3f;

        //private float baseMoveSpeed;
        //
        //public float DamageSpeed => damageSpeed;

        protected bool isAbilityOneReady = true;
        protected bool isAbilityTwoReady = true;
        protected BreadInput breadInput;
        protected BreadMovementController breadMovementController;
        
        private BreadState breadState;
        
        public BreadState BreadState => breadState;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            breadInput = GetComponent<BreadInput>();
            breadMovementController = GetComponent<BreadMovementController>();
            health = maxHealth;
        }

        public virtual void Destroyed()
        {
            //TODO: Disable current gameobject and input. Set camera follow to other players.
            //Be able to cycle through players for spectate.

            //TODO: Add possible 3rd camera for spectate mode.
            NetworkServer.Destroy(gameObject);
        }

        public virtual void TakeDamage()
        {
            CmdTakeDamage();

            if (health <= 0)
            {
                Destroyed();
            }

            //StartCoroutine(MoveSpeedDecay(baseMoveSpeed, damageSpeed, damagedSpeedTimeBeforeDecay, damagedSpeedDecayTime));
        }

        public void SetState(BreadState newState)
        {
            breadState = newState;
        }

        [Command]
        protected void CmdTakeDamage()
        {
            health--;
        }

        protected virtual IEnumerator AbilityOneCooldown(float seconds)
        {
            if (isAbilityOneReady == false) yield break;

            isAbilityOneReady = false;
            yield return new WaitForSeconds(seconds);
            isAbilityOneReady = true;
        }

        protected virtual IEnumerator AbilityTwoCooldown(float seconds)
        {
            if (isAbilityTwoReady == false) yield break;

            isAbilityTwoReady = false;
            yield return new WaitForSeconds(seconds);
            isAbilityTwoReady = true;
        }

        //IEnumerator MoveSpeedDecay(float baseSpeed, float damagedSpeed, float timeBeforeDecay, float decayTime)
        //{
        //    float timeElapsed = 0f;
        //    controller.MoveSpeed = damagedSpeed;
        //
        //    yield return new WaitForSeconds(timeBeforeDecay);
        //
        //    while (timeElapsed < decayTime)
        //    {
        //        controller.MoveSpeed = Mathf.Lerp(damagedSpeed, baseSpeed, timeElapsed / decayTime);
        //        timeElapsed += Time.deltaTime;
        //
        //        yield return null;
        //    }
        //
        //    controller.MoveSpeed = baseSpeed;
        //}
    }
}
