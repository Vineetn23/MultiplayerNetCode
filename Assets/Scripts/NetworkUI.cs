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


    // Start is called before the first frame update
    void Start()
    {
        hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
