using Mirror;
using System;
using UnityEngine;

public class WMBNetworkManager : NetworkRoomManager
{
    public event Action<bool> OnPlayersReadyStatusChanged;
    public CharacterListSO characterList;
    bool showStartButton;

    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        showStartButton = true;
        OnPlayersReadyStatusChanged?.Invoke(true);
#endif
    }

    public override void OnRoomServerPlayersNotReady()
    {
#if UNITY_SERVER
            base.OnRoomServerPlayersNotReady();
#else
        showStartButton = false;
        OnPlayersReadyStatusChanged?.Invoke(false);
#endif
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);

    }

    //public override void OnGUI()
    //{
    //    base.OnGUI();
    //
    //    if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
    //    {
    //        // set to false to hide it in the game scene
    //        showStartButton = false;
    //
    //        ServerChangeScene(GameplayScene);
    //    }
    //}
}

public struct PlayerCreationMessage : NetworkMessage
{

}

public enum CharacterChoice { }

