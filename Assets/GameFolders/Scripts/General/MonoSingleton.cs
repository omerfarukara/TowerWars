using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static volatile T instance = null; // volatile -> Degiskenin degerinin direkt bellekten alinmasini saglar.

    public static T Instance => instance;

    protected void Singleton(bool dontDestroyOnLoad = false)
    {
        if (dontDestroyOnLoad)
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (instance == null)
            {
                instance = this as T;
            }
        }
    }
}