using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WMBNetworkManager : NetworkRoomManager
{
    //TODO: Be sure to clear/deque list when client leaves or server stops
    public static Dictionary<int, PlayerInfo> players;
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

    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);
        if (players.ContainsKey(conn.connectionId))
            return;

        players.Add(conn.connectionId, new PlayerInfo());
        Debug.Log(players.Count);
    }

    public override void OnRoomServerDisconnect(NetworkConnection conn)
    {
        base.OnRoomClientDisconnect(conn);
        players.Remove(conn.connectionId);
    }

    //TODO: If still autocreate, need way to change spawn positions for bread/gura
    public override void OnRoomStartServer()
    {
        base.OnRoomStartServer();

        players = new Dictionary<int, PlayerInfo>();
    }

    //TODO: Sets at other spawn positions. Doesn't really fix the problem, but works for now.
    public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
    {
        //Instantiate new player gameobject to be spawned by the server on scene change
        roomPlayer = Instantiate(characterList.Characters[players[conn.connectionId].CharacterChoice], GetStartPosition().position, GetStartPosition().rotation);
        return roomPlayer;
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

    public string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }

    public int CharacterChoice
    {
        get => characterChoice;
        set => characterChoice = value;
    }
}

