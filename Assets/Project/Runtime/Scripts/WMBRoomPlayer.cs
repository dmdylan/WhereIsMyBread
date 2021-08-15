using UnityEngine;
using Mirror;
using System;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-room-player
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
/// </summary>
public class WMBRoomPlayer : NetworkRoomPlayer
{
    public event Action<bool> OnPlayerReadyStatusChanged;
    public event Action<string> OnPlayerNameChanged;
    public event Action<int> OnPlayerCharacterChanged;

    [SerializeField] private GameObject playerUIPanelPrefab;
    [SerializeField] private GameObject playerUIButtonsPrefab;
    [SerializeField] private GameObject playerNamePanelPrefab;
    private GameObject playerUIPanel;
    private GameObject playerUIButtons;
    private GameObject playerNamePanelUI;

    [SyncVar(hook = nameof(PlayerNameChanged))]
    string playerName;

    [SyncVar(hook = nameof(PlayerCharacterChanged))]
    int playerCharacterNumber;

    public void ChangeReadyStatus(bool playerReadyStatus)
    { 
        if(NetworkClient.active && isLocalPlayer)
        {
            CmdChangeReadyState(playerReadyStatus);
        }
        else if (NetworkClient.isHostClient)
        {
            CmdChangeReadyState(playerReadyStatus);
        }
    }

    public void ChangePlayerName(string newName)
    {
        if(NetworkClient.active && isLocalPlayer)
        {
            CmdChangeName(newName);
        }
    }

    [Command]
    private void CmdChangeName(string newName)
    {
        playerName = newName;
    }

    public void ChangePlayerCharacterNumber(int newCharacter)
    {
        if(NetworkClient.active && isLocalPlayer)
        {
            CmdChangeCharacter(newCharacter);
        }
    }

    [Command]
    private void CmdChangeCharacter(int newCharacter)
    {
        playerCharacterNumber = newCharacter;
    }

    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() { }

    /// <summary>
    /// Invoked on the server when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    /// </summary>
    public override void OnStopServer() { }

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient() 
    {
        playerUIPanel = Instantiate(playerUIPanelPrefab, GameObject.FindGameObjectWithTag("PlayerPanel").transform);
        playerUIPanel.GetComponent<LobbyPanelUI>().SetPlayer(this, isLocalPlayer, playerName);

        if (isLocalPlayer)
        {
            playerNamePanelUI = Instantiate(playerNamePanelPrefab, GameObject.Find("Canvas").transform);
            playerNamePanelUI.GetComponent<PlayerNamePanelUI>().PlayerSetup(this);
            playerUIButtons = Instantiate(playerUIButtonsPrefab, GameObject.Find("Canvas").transform);
            playerUIButtons.GetComponent<LobbyButtonsUI>().SetPlayer(this, isLocalPlayer);
        }

        OnPlayerReadyStatusChanged?.Invoke(readyToBegin);
        OnPlayerCharacterChanged?.Invoke(playerCharacterNumber);
    }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient() 
    {
        Destroy(playerUIPanel);
    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer() { }

    /// <summary>
    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority"/> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnection parameter included, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStartAuthority() { }

    /// <summary>
    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStopAuthority() { }

    #endregion

    #region Room Client Callbacks

    /// <summary>
    /// This is a hook that is invoked on all player objects when entering the room.
    /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
    /// </summary>
    public override void OnClientEnterRoom() { }

    /// <summary>
    /// This is a hook that is invoked on all player objects when exiting the room.
    /// </summary>
    public override void OnClientExitRoom() { }

    #endregion

    #region SyncVar Hooks

    /// <summary>
    /// This is a hook that is invoked on clients when the index changes.
    /// </summary>
    /// <param name="oldIndex">The old index value</param>
    /// <param name="newIndex">The new index value</param>
    public override void IndexChanged(int oldIndex, int newIndex) { }

    /// <summary>
    /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    /// <para>This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().</para>
    /// </summary>
    /// <param name="oldReadyState">The old readyState value</param>
    /// <param name="newReadyState">The new readyState value</param>
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) 
    {
        OnPlayerReadyStatusChanged?.Invoke(newReadyState);
    }

    void PlayerNameChanged(string oldName, string newName)
    {
        OnPlayerNameChanged?.Invoke(newName);
    }

    void PlayerCharacterChanged(int oldCharacterNumber, int newCharacterNumber)
    {
        OnPlayerCharacterChanged?.Invoke(newCharacterNumber);
    }

    #endregion

    //#region Optional UI
    //
    //public override void OnGUI()
    //{
    //    base.OnGUI();
    //}
    //
    //#endregion
}
