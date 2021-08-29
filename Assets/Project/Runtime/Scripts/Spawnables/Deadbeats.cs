using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadbeats : NetworkBehaviour
{
    [SerializeField] private AbilitySO calliDeadbeatsAbility;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawned());
    }
    
    IEnumerator Spawned()
    {
        yield return new WaitForSeconds(calliDeadbeatsAbility.AbilityDuration);
        NetworkServer.Destroy(gameObject);
    }
}
