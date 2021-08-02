using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadStuff
{
    public abstract class Bread : NetworkBehaviour, IDamageable
    {
        [SyncVar]
        [SerializeField] protected int health;

        [SerializeField] private int maxHealth = 2;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

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
        }

        [Command]
        protected void CmdTakeDamage()
        {
            health--;
        }
    }
}
