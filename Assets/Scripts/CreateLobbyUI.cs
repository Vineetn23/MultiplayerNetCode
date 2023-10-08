using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLobbyUI : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Slider maxPlayerSlider;
    public Button createLobbyButton;


    void Start()
    {
        createLobbyButton.onClick.AddListener(CreateLobbyFromUI);
    }

    public void CreateLobbyFromUI()
    {
        LobbyManager.LobbyData lobbyData = new LobbyManager.LobbyData();
        lobbyData.maxPlayer = (int)maxPlayerSlider.value;
        lobbyData.lobbyName = nameInputField.text;

        LobbyManager.Instance.CreateLobby(lobbyData);
    }

}
