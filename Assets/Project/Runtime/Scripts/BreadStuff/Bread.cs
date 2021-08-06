using Mirror;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    enum BreadState { Moving, Jumping, Dead }

    public abstract class Bread : NetworkBehaviour, IDamageable
    {
        [SyncVar]
        [SerializeField] protected int health;

        [SerializeField] private int maxHealth = 2;
        [SerializeField] private float damageSpeed = 8f;
        [SerializeField] private float damagedSpeedTimeBeforeDecay = 2f;
        [SerializeField] private float damagedSpeedDecayTime = 3f;

        private ThirdPersonController controller;
        private float baseMoveSpeed;

        public float DamageSpeed => damageSpeed;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            health = maxHealth;
            controller = GetComponent<ThirdPersonController>();
            baseMoveSpeed = controller.MoveSpeed;
        }

        public virtual void Destroyed()
        {
            //TODO: Disable current gameobject and input. Set camera follow to other players.
            //Be able to cycle through players for spectate.

            //TODO: Add possible 3rd camera for spectate mode.
            NetworkServer.Destroy(gameObject);
        }

        void OnThrow()
        {
            TakeDamage();
        }

        public virtual void TakeDamage()
        {
            //CmdTakeDamage();

            if (health <= 0)
            {
                Destroyed();
            }

            StartCoroutine(MoveSpeedDecay(baseMoveSpeed, damageSpeed, damagedSpeedTimeBeforeDecay, damagedSpeedDecayTime));
        }

        [Command]
        protected void CmdTakeDamage()
        {
            health--;
        }

        IEnumerator MoveSpeedDecay(float baseSpeed, float damagedSpeed, float timeBeforeDecay, float decayTime)
        {
            float timeElapsed = 0f;
            controller.MoveSpeed = damagedSpeed;

            yield return new WaitForSeconds(timeBeforeDecay);

            while (timeElapsed < decayTime)
            {
                controller.MoveSpeed = Mathf.Lerp(damagedSpeed, baseSpeed, timeElapsed / decayTime);
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            controller.MoveSpeed = baseSpeed;
        }
    }
}
