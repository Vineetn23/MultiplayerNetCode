using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;

public class RelayManager : MonoBehaviour
{
    private UnityTransport transport;

    public static RelayManager Instance;

    private void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        transport = GetComponent<UnityTransport>();
    }

    public async void CreateRelayGame(int maxPlayer)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);

        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        Debug.Log("THE JOIN CODE IS : " + joinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,  allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.StartHost();

    }

    public async void JoinRelayGame(string joinCode)
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);


        NetworkManager.Singleton.StartClient();
    }

}
