using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject[] playerPanels;
    WMBNetworkManager networkManager;
    NetworkConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        networkManager = FindObjectOfType<WMBNetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
