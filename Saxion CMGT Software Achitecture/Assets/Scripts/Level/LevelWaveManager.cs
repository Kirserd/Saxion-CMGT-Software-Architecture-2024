using static LevelWaveData.WaveEvent;

public class LevelWaveManager
{
    public delegate void OnWaveEventHandler(Wave wave);
    public OnWaveEventHandler OnWaveEvent;

    private readonly (float Delay, Wave Wave)[] _sortedByDelayWaveEventsPrefab;

    private int _lastWaveIndex;
    private float _passedTimeBuffer;

    public LevelWaveManager(LevelWaveData waveData)
    {
        var sortedByDelay = waveData.GetSortedByDelay();
        _sortedByDelayWaveEventsPrefab = new (float Delay, Wave Wave)[sortedByDelay.Length];
        sortedByDelay.CopyTo(_sortedByDelayWaveEventsPrefab, 0);

        Reset();
    }

    public void Reset() 
    {
        if (_sortedByDelayWaveEventsPrefab is null)
            return;

        _lastWaveIndex    = -1;
        _passedTimeBuffer =  0;
    }
    public void Next(float passedTime)
    {
        if (_lastWaveIndex + 1 >= _sortedByDelayWaveEventsPrefab.Length)
            return;

        _passedTimeBuffer += passedTime;
        float sampleDelay = _sortedByDelayWaveEventsPrefab[_lastWaveIndex + 1].Delay;
        if (_passedTimeBuffer >= sampleDelay)
        {
            _passedTimeBuffer -= sampleDelay;
            _lastWaveIndex++;

            OnWaveEvent?.Invoke(_sortedByDelayWaveEventsPrefab[_lastWaveIndex].Wave);
        }
    }
}
