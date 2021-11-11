using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericSingletonClass<T> : MonoBehaviour where T : Component
{
    private static bool applicationIsQuitting = false;
    private static T instance;
    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += () => applicationIsQuitting = true;
    }
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public bool GetApplicationIsQuitting() => applicationIsQuitting;
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}