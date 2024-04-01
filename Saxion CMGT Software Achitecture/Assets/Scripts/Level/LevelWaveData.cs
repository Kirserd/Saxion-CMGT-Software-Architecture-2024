using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelWaveData", menuName = "Static Data/Level Data/Wave Data", order = 0)]
public class LevelWaveData : ScriptableObject
{
    public WaveEvent[] WaveEvents;
    private (float Delay, WaveEvent.Wave Wave)[] _sortedByDelayWaveEvents;

    [Serializable]
    public class WaveEvent
    {
        public float Timestamp;
        public Wave Event;

        [Serializable]
        public class Wave
        {
            public float Amount;
            public Creature Creature; 
        }
    }

    public (float Delay, WaveEvent.Wave Wave)[] GetSortedByDelay() => _sortedByDelayWaveEvents;
    private void OnValidate()
    {
        SortedList<float, WaveEvent.Wave> sortedByTimestampWaveEvents = new(WaveEvents.Length);
        foreach (var waveEvent in WaveEvents)
        {
            float virtualTimestamp = waveEvent.Timestamp;
            while (sortedByTimestampWaveEvents.ContainsKey(virtualTimestamp))
                virtualTimestamp += 0.01f;

            sortedByTimestampWaveEvents.Add(virtualTimestamp, waveEvent.Event);
        }

        _sortedByDelayWaveEvents = new (float Delay, WaveEvent.Wave Wave)[WaveEvents.Length];

        float prevTimestamp = 0;
        int iterator = 0;
        foreach (var waveEvent in sortedByTimestampWaveEvents)
        {
            _sortedByDelayWaveEvents[iterator] = (waveEvent.Key - prevTimestamp, waveEvent.Value);
            prevTimestamp = waveEvent.Key;
            iterator++;
        }

        sortedByTimestampWaveEvents.Clear();

        int errors = 0;
        foreach (var waveEvent in _sortedByDelayWaveEvents)
            if(waveEvent.ToTuple() == null)
                errors++;

        if(errors > 0)
            Debug.Log($"Successfully created sorted by delay wave events with {errors} errors");
    }
}
