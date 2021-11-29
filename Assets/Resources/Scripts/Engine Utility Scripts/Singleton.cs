using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Instance;

    public void CheckInstance(T genericInstance,  bool destroyObject = false)
    {
        if (Instance != null)
        {
            if (destroyObject)
                Destroy(gameObject);
            else
                Destroy(this);
        }
        else
            Instance = genericInstance;
    }

    public void RemoveInstance(bool destroyObject = false)
    {
        Instance = default;

        if (destroyObject)
            Destroy(gameObject);
        else
            Destroy(this);
    }



}
