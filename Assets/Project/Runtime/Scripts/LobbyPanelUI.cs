using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LobbyPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject characterSelectButtons;
    [SerializeField] private GameObject playerReadyImage;
    [SerializeField] private GameObject playerNotReadyImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private GameObject[] characterChoices;

    WMBRoomPlayer player;

    public void SetPlayer(WMBRoomPlayer player, bool isLocalPlayer)
    {
        this.player = player;

        player.OnPlayerReadyStatusChanged += Player_OnPlayerReadyStatusChanged;
        player.OnPlayerNameChanged += Player_OnPlayerNameChanged;

        if (isLocalPlayer)
        {
            characterSelectButtons.SetActive(true);
        }
    }

    private void OnDisable()
    {
        player.OnPlayerReadyStatusChanged -= Player_OnPlayerReadyStatusChanged;
        player.OnPlayerNameChanged -= Player_OnPlayerNameChanged;
    }

    private void Player_OnPlayerNameChanged(string newName)
    {
        playerNameText.text = newName;
    }

    private void Player_OnPlayerReadyStatusChanged(bool readyStatus)
    {
        if(readyStatus == false)
        {
            playerNotReadyImage.SetActive(true);
            playerReadyImage.SetActive(false);
        }
        else if(readyStatus == true)
        {
            playerNotReadyImage.SetActive(false);
            playerReadyImage.SetActive(true);
        }
    }
}
