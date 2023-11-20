using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("Singleton of" + (T)this + "is Trying to Create Second Instance");
        }
    }

}
