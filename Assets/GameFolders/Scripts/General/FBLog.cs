using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FBLog : MonoBehaviour
{
    private void Awake ()
    {
        if (FB.IsInitialized) 
        {
            FB.ActivateApp();
        } 
        else 
        {
            FB.Init( () => {
                FB.ActivateApp();
            });
        }
    }
}