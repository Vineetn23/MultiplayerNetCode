using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawnManager : NetworkBehaviour
{
    public Transform player;
    public Transform[] spawnPositions;
    private NetworkVariable<int> networkIndex = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public void SetPlayerPosition()
    {
        int index = networkIndex.Value;
        player.position = spawnPositions[index].position;
        player.rotation = spawnPositions[index].rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
    }

    private void Singleton_OnServerStarted()
    {
        if (IsServer)
        {
            networkIndex.Value++;
            if (networkIndex.Value == spawnPositions.Length)
            {
                networkIndex.Value = 0;
            }
        }
    }

    private void Singleton_OnClientConnectedCallback(ulong clientID)
    {
        if(clientID == NetworkManager.Singleton.LocalClientId)
        {
            SetPlayerPosition();
        }

        if (IsServer)
        {
            networkIndex.Value++;
            if (networkIndex.Value == spawnPositions.Length)
            {
                networkIndex.Value = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
