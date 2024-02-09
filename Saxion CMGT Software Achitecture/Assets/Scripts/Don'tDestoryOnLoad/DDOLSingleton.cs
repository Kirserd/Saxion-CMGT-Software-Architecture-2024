using UnityEngine;

public class DDOLSingleton : MonoBehaviour
{
    public static DDOLSingleton Instance { get; protected set; }
    protected virtual void Awake()
    {
        if (Instance is not null)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }
}
