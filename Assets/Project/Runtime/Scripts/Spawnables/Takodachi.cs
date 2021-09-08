using Mirror;
using System.Collections;
using UnityEngine;
using BreadStuff;
using DG.Tweening;

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
            CmdTakoTween();
        }
    }

    IEnumerator DestroyTheTako()
    {
        yield return new WaitForSeconds(takodachiAbility.AbilityDuration);
        DOTween.Clear();
        NetworkServer.Destroy(gameObject);
    }

    [Command(requiresAuthority = false)]
    void CmdTakoTween(NetworkConnectionToClient sender = null)
    {
        transform.DOShakeScale(.5f, 1f, 5, 30, true);
    }
}
