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

        private ThirdPersonController controller;

        public float DamageSpeed => damageSpeed;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            health = maxHealth;
            controller = GetComponent<ThirdPersonController>();
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

            StartCoroutine(MoveSpeedDecay());
        }

        [Command]
        protected void CmdTakeDamage()
        {
            health--;
        }

        IEnumerator MoveSpeedDecay()
        {
            float temp = controller.MoveSpeed;
            controller.MoveSpeed = damageSpeed;

            yield return new WaitForSeconds(2f);

            while(controller.MoveSpeed >= temp)
            {
                controller.MoveSpeed = Mathf.Lerp(damageSpeed, temp, 3f * Time.deltaTime);
            }
        }
    }
}
