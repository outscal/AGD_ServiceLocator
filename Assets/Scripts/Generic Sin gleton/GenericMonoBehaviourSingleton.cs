using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoBehaviourSingleton<T> : MonoBehaviour where T : GenericMonoBehaviourSingleton<T>
{

    private static T instance;
    public static T Instance { get { return instance; } }


    private void Awake()
    {
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
