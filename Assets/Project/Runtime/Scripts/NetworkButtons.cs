using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkButtons : MonoBehaviour
{
    NetworkManager networkManager;

    // Start is called before the first frame update
    void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    public void StartHostGame()
    {
        if (!NetworkClient.active)
        {
            networkManager.StartHost();
        }
    }

    public void StartClientGame()
    {
        if (!NetworkClient.active)
        {
            networkManager.StartClient();
            //TODO: Set the network address from the input field
            //networkManager.networkAddress = 
        }
    }
    
    public void StopHostGame()
    {
        if(NetworkServer.active && NetworkClient.active)
        {
            networkManager.StopHost();
        }
    }

    public void StopClientGame()
    {
        if (NetworkClient.isConnected)
        {
            networkManager.StopClient();
        }
    }

    public void SetNetworkAddress(TMP_InputField inputField)
    {
        networkManager.networkAddress = inputField.text;
    } 
}
