using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    //[SerializeField]
    //private LevelWaveData 

    public LevelResources Resources { get; private set; }
    public LevelWaveManager WaveManager { get; private set; }

    public void Awake()
    {
        Instance = this;

        Resources = new(startAmount: 500);
        WaveManager = new();
    }
}
