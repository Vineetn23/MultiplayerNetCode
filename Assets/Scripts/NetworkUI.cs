using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{

    public Button hostButton;
    public Button serverButton;
    public Button clientButton;

    public string joinCode;


    // Start is called before the first frame update
    void Start()
    {
        hostButton.onClick.AddListener(() => RelayManager.Instance.CreateRelayGame(20));
        //serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        clientButton.onClick.AddListener(() => RelayManager.Instance.JoinRelayGame(joinCode));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
