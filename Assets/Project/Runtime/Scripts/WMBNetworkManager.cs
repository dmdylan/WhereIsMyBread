using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WMBNetworkManager : NetworkRoomManager
{
    //TODO: Be sure to clear/deque list when client leaves or server stops
    public static Dictionary<uint, PlayerInfo> players;
    public event Action<bool> OnPlayersReadyStatusChanged;
    public CharacterListSO characterList;

    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        OnPlayersReadyStatusChanged?.Invoke(true);
#endif
    }

    public override void OnRoomServerPlayersNotReady()
    {
#if UNITY_SERVER
            base.OnRoomServerPlayersNotReady();
#else
        OnPlayersReadyStatusChanged?.Invoke(false);
#endif
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);

    }

    //TODO: Might need to even do this. Might be able to auto set player prefab prior to joining if it doesn't impact all
    //clients connected to the host/network manager.

    //TODO: If still autocreate, need way to change spawn positions for bread/gura
    public override void OnRoomStartServer()
    {
        base.OnRoomStartServer();

        players = new Dictionary<uint, PlayerInfo>();
    }

    //TODO: Use OnRoomServerAddPlayer

    //public override void OnRoomServerAddPlayer(NetworkConnection conn)
    //{
    //    NetworkServer.ReplacePlayerForConnection(conn, Instantiate(characterList.Characters[1]));//players[(uint)conn.connectionId].CharacterChoice]));
    //    //base.OnRoomServerAddPlayer(conn);
    //}

    public override void OnServerChangeScene(string newSceneName)
    {
        if(newSceneName == GameplayScene)
        {
            foreach (var player in players)
            {
                //if (NetworkServer.active)
                //{
                NetworkServer.ReplacePlayerForConnection(player.Value.Conn, Instantiate(characterList.Characters[1]));//player.Value.CharacterChoice]));
                //}
            }
        }
    
        base.OnServerChangeScene(newSceneName);
    }
}

public struct PlayerInfo
{
    private NetworkConnection conn;
    private string playerName;
    private int characterChoice;

    public PlayerInfo(NetworkConnection conn, string playerName, int characterChoice)
    {
        this.conn = conn;
        this.playerName = playerName;
        this.characterChoice = characterChoice;
    }

    public NetworkConnection Conn => conn;
    public string PlayerName => playerName;
    public int CharacterChoice
    {
        get => characterChoice;
        set => characterChoice = value;
    }
}

