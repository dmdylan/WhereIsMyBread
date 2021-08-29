using Mirror;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BreadStuff
{
    public enum BreadState { Grounded, Falling, Dead }

    public abstract class Bread : NetworkBehaviour, IDamageable
    {
        //Health
        [SyncVar]
        [SerializeField] protected int health;
        [SerializeField] protected int maxHealth = 2;

        //On hit
        [SerializeField] private BreadDamagedSO breadDamaged;

        //Protected vars
        protected bool isAbilityOneReady = true;
        protected bool isAbilityTwoReady = true;
        protected BreadInput breadInput;
        protected BreadMovementController breadMovementController;
        
        private BreadState breadState;
        private float baseMoveSpeed;
        private PlayerInput playerInput;
        
        public BreadState BreadState => breadState;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            playerInput = GetComponent<PlayerInput>();
            playerInput.enabled = true;
            breadInput = GetComponent<BreadInput>();
            breadMovementController = GetComponent<BreadMovementController>();
            health = maxHealth;
            baseMoveSpeed = breadMovementController.MoveSpeed;
            //TODO: Move this somewhere smart
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
            else
            {
                StartCoroutine(MoveSpeedDecay());
            }
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

        protected IEnumerator MoveSpeedDecay()
        {
            float timeElapsed = 0f;
            breadMovementController.MoveSpeed *= breadDamaged.DamagedSpeedMultiplier;
        
            yield return new WaitForSeconds(breadDamaged.DamagedSpeedTimeBeforeDecay);
        
            while (timeElapsed < breadDamaged.DamagedSpeedDecayTime)
            {
                breadMovementController.MoveSpeed = Mathf.Lerp(breadDamaged.DamagedSpeedMultiplier, baseMoveSpeed, timeElapsed / breadDamaged.DamagedSpeedDecayTime);
                timeElapsed += Time.deltaTime;
        
                yield return null;
            }

            breadMovementController.MoveSpeed = baseMoveSpeed;
        }
    }
}
