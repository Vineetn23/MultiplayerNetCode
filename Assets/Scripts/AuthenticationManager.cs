using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.Events;


#if UNITY_EDITOR
using ParrelSync;
#endif

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance;
    public UnityEvent SignIn;

    private void Awake()
    {
        Instance = this;
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

        SignIn.Invoke();
    }
}
