using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else if (Instance != this as T)
        {
            Debug.LogError($"More than one Instance of {Instance} {typeof(T)}");
            Destroy(this);
        }
    }
}
