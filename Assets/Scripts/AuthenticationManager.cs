using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;


#if UNITY_EDITOR
using ParrelSync;
#endif

public class AuthenticationManager : MonoBehaviour
{
    private void Awake()
    {
        Login();
    }

    public async void Login()
    {
        InitializationOptions options = new InitializationOptions();

#if UNITY_EDITOR
        if (ClonesManager.IsClone())
        {
            options.SetProfile(ClonesManager.GetArgument());
        }
        else
        {
            options.SetProfile("primary");
        }
#endif


        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
