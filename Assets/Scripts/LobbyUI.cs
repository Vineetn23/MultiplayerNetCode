using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;

public class LobbyUI : MonoBehaviour
{
    public GameObject authentication;

    public GameObject lobbyMenu;
    public GameObject createLobby;
    public GameObject lobbyList;

    public Button quickJoinButton;
    public Button createLobbyButton;
    public Button lobbyListButton;

    public void UIEnabler(int index)
    {
        GameObject[] uiElements = new GameObject[] { lobbyMenu, createLobby, lobbyList, authentication };

        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == index);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIEnabler(3);

        AuthenticationManager.Instance.SignIn.AddListener (() => UIEnabler(0));

        quickJoinButton.onClick.AddListener(() => LobbyManager.Instance.QuickJoinLobby());
        createLobbyButton.onClick.AddListener(() => UIEnabler(1));
        lobbyListButton.onClick.AddListener(() => UIEnabler(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
