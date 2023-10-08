using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public struct LobbyData
    {
        public string lobbyName;
        public int maxPlayer;
    }
    public async void CreateLobby(LobbyData lobbyData)
    {
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;
        lobbyOptions.Data = new Dictionary<string, DataObject>();

        string joinCode = await RelayManager.Instance.CreateRelayGame(lobbyData.maxPlayer);


        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, joinCode);
        lobbyOptions.Data.Add("Join Code Key", dataObject);

        await Lobbies.Instance.CreateLobbyAsync(lobbyData.lobbyName, lobbyData.maxPlayer, lobbyOptions);
    }

    public async void QuickJoinLobby()
    {
        Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        string relayJoinCode = lobby.Data["Join Code Key"].Value;
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }

    public async void JoinLobby(string lobbyId)
    {
        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
        string relayJoinCode = lobby.Data["Join Code Key"].Value;

        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
