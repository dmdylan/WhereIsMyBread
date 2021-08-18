using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButtonsUI : MonoBehaviour
{
    [SerializeField] private GameObject startGameButton;

    WMBRoomPlayer player;
    bool isLocalPlayer;
    bool isPlayerReady;

    public void SetPlayer(WMBRoomPlayer player, bool isLocalPlayer)
    {
        this.player = player;
        this.isLocalPlayer = isLocalPlayer;

        if (player.hasAuthority)
        {
            ((WMBNetworkManager)NetworkManager.singleton).OnPlayersReadyStatusChanged += LobbyButtonsUI_OnPlayersReadyStatusChanged;
        }
    }

    private void OnDisable()
    {
        ((WMBNetworkManager)NetworkManager.singleton).OnPlayersReadyStatusChanged -= LobbyButtonsUI_OnPlayersReadyStatusChanged;
    }

    private void LobbyButtonsUI_OnPlayersReadyStatusChanged(bool areAllPlayersReady)
    {
        startGameButton.SetActive(areAllPlayersReady);
    }

    public void OnReadyButton()
    {
        isPlayerReady = !isPlayerReady;
        player.ChangeReadyStatus(isPlayerReady);
    }

    public void OnBackButton()
    {
        if (player.hasAuthority)
            NetworkManager.singleton.StopHost();
        else if (isLocalPlayer)
            NetworkManager.singleton.StopClient();
    }

    public void OnStartGameButton()
    {
        NetworkManager.singleton.ServerChangeScene(((WMBNetworkManager)NetworkManager.singleton).GameplayScene);
        //((WMBNetworkManager)NetworkManager.singleton).ServerChangeScene(((WMBNetworkManager)NetworkManager.singleton).GameplayScene);
    }
}
