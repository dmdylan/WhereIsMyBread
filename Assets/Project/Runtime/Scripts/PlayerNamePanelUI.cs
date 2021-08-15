using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerNamePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;

    WMBRoomPlayer player;

    public void PlayerSetup(WMBRoomPlayer player)
    {
        this.player = player;
    }

    public void OnAcceptButton()
    {
        player.ChangePlayerName(nameInputField.text);
        this.gameObject.SetActive(false);
    }
}
