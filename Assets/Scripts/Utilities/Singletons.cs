using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A static instance is similar to a singleton, but instead of destroying any new
/// instances, it overrides the current instance. This is handy for resetting the state
/// and saves you doing it manually.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AStaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}


/// <summary>
/// This transforms the static instance into a basic singleton. This will destroy any new
/// versions created, leaving the original instance intact.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ASingleton<T> : AStaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }
}


/// <summary>
/// Finally we have a persistent version of the Singleton. This will survive through scene
/// loads. Perfect for system classes which require stateful, persistent data. Or audio sources 
/// where music plays through loading screens, etc
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class APersistentSingleton<T> : ASingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
