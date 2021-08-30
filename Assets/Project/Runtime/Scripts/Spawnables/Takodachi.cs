using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreadStuff;

public class Takodachi : NetworkBehaviour
{
    [SerializeField] private AbilitySO takodachiAbility;

    private void Start()
    {
        StartCoroutine(DestroyTheTako());
    }

    //TODO: Inconsistent when adding force. Probably due to movement vectors adding up incorrectly.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Bread bread))
        {
            bread.PlayerRigidbody.AddForce(Vector3.up * takodachiAbility.AbilityEffectFloat, ForceMode.Impulse);
        }
    }

    IEnumerator DestroyTheTako()
    {
        yield return new WaitForSeconds(takodachiAbility.AbilityDuration);
        NetworkServer.Destroy(gameObject);
    }
}
