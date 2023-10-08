using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class NetworkSetRandomColor : NetworkBehaviour
{

    public List<Renderer> renderers;
    private NetworkVariable<Color> networkColor = new NetworkVariable<Color>(Color.blue, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public void SetRendererColor(Color newColor)
    {
        foreach (var items in renderers)
        {
            items.material.color = newColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        networkColor.OnValueChanged += (x, y) => SetRendererColor(y);

        if (IsOwner)
        {
            networkColor.Value = Random.ColorHSV(0, 1, 1, 1);
        }
        SetRendererColor(networkColor.Value);
    }

    public override void OnNetworkDespawn()
    {
        networkColor.OnValueChanged -= (x, y) => SetRendererColor(y);
    }

    public override void OnNetworkSpawn()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RequestUpdateColorServerRpc(new ServerRpcParams());
        }
    }

    [ClientRpc]
    public void UpdateColorClientRpc(ClientRpcParams clientParameters)
    {
        if (IsOwner)
        {
            networkColor.Value = Random.ColorHSV(0, 1, 1, 1);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestUpdateColorServerRpc(ServerRpcParams parameters)
    {
        Debug.Log("Sent by " + parameters.Receive.SenderClientId);

        ClientRpcParams clientParameters = new ClientRpcParams();
        clientParameters.Send.TargetClientIds = new List<ulong>() { 0 };

        UpdateColorClientRpc(clientParameters);
    }
}
