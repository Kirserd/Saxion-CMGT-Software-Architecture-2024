using UnityEngine;

public class ControlsProxy : MonoBehaviour
{
    public static ControlsProxy Instance { get; protected set; }
    private void Awake()
    {
        if (Instance is not null)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update() => Controls.Update();
}