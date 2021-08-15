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
    bool isLocalPlayer;
    int currentPlayerCharacter = 0;

    public void SetPlayer(WMBRoomPlayer player, bool isLocalPlayer, string playerName)
    {
        this.player = player;
        this.isLocalPlayer = isLocalPlayer;

        playerNameText.text = playerName;

        player.OnPlayerReadyStatusChanged += Player_OnPlayerReadyStatusChanged;
        player.OnPlayerNameChanged += Player_OnPlayerNameChanged;
        player.OnPlayerCharacterChanged += Player_OnPlayerCharacterChanged;

        if (isLocalPlayer)
        {
            characterSelectButtons.SetActive(true);
        }
    }

    private void OnDisable()
    {
        player.OnPlayerReadyStatusChanged -= Player_OnPlayerReadyStatusChanged;
        player.OnPlayerNameChanged -= Player_OnPlayerNameChanged;
        player.OnPlayerCharacterChanged -= Player_OnPlayerCharacterChanged;
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

            if(isLocalPlayer)
                characterSelectButtons.SetActive(true);
        }
        else if(readyStatus == true)
        {
            playerNotReadyImage.SetActive(false);
            playerReadyImage.SetActive(true);

            if(isLocalPlayer)
                characterSelectButtons.SetActive(false);
        }
    }

    private void Player_OnPlayerCharacterChanged(int characterNumber)
    {
        switch (characterNumber)
        {
            case 0:
                LoopThroughPlayerModelsAndTurnOff();
                characterChoices[0].SetActive(true);
                break;
            case 1:
                LoopThroughPlayerModelsAndTurnOff();
                characterChoices[1].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnNextCharacterButton()
    {
        if(currentPlayerCharacter >= characterChoices.Length - 1)
        {
            currentPlayerCharacter = 0;
            player.ChangePlayerCharacterNumber(currentPlayerCharacter);
        }
        else
        {
            currentPlayerCharacter++;
            player.ChangePlayerCharacterNumber(currentPlayerCharacter);
        }
    }

    public void OnPreviousCharacterButton()
    {
        if (currentPlayerCharacter <= 0)
        {
            currentPlayerCharacter = characterChoices.Length - 1;
            player.ChangePlayerCharacterNumber(currentPlayerCharacter);
        }
        else
        {
            currentPlayerCharacter--;
            player.ChangePlayerCharacterNumber(currentPlayerCharacter);
        }
    }

    private void LoopThroughPlayerModelsAndTurnOff()
    {
        foreach (GameObject model in characterChoices)
            model.SetActive(false);
    }
}
