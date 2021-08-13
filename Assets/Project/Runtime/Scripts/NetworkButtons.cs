using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkButtons : MonoBehaviour
{
    WMBNetworkManager wmbNetworkManager;

    // Start is called before the first frame update
    void Awake()
    {
        wmbNetworkManager = GetComponent<WMBNetworkManager>();
    }

    public void StartHostGame()
    {
        if (!NetworkClient.active)
        {
            wmbNetworkManager.StartHost();
        }
    }

    public void StartClientGame()
    {
        if (!NetworkClient.active)
        {
            wmbNetworkManager.StartClient();
            //TODO: Set the network address from the input field
            //networkManager.networkAddress = 
        }
    }
    
    public void StopHostGame()
    {
        if(NetworkServer.active && NetworkClient.active)
        {
            wmbNetworkManager.StopHost();
        }
    }

    public void StopClientGame()
    {
        if (NetworkClient.isConnected)
        {
            wmbNetworkManager.StopClient();
        }
    }

    public void SetNetworkAddress(TMP_InputField inputField)
    {
        wmbNetworkManager.networkAddress = inputField.text;
    }
    
    public void SetPlayerName(TMP_InputField inputField)
    {

    }
}
