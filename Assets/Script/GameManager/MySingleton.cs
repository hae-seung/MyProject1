using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingleton<T>: MonoBehaviour where T : MonoBehaviour //Just In Scene
{
    private static T instance;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }
    
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
            Destroy(gameObject);
    }
}
