using UnityEngine;

public class RenderImageScaler : MonoBehaviour
{
    public static RenderImageScaler Instance { get; protected set; }

    [SerializeField] 
    private RenderTexture _render;

    private Vector2Int _previousScreenSize;

    private void Awake()
    {
        if (Instance is not null)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private void LateUpdate()
    {
        Vector2Int currentScreenSize = new Vector2Int(Screen.width, Screen.height);
        if(_previousScreenSize != currentScreenSize)
        {
            _previousScreenSize = currentScreenSize;
            
            _render.Release();
            _render.width = currentScreenSize.x;
            _render.height = currentScreenSize.y;
            _render.Create();
        }
    }
}