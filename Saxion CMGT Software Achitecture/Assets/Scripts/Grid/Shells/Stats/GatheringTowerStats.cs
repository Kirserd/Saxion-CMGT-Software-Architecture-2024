using System;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class GatheringTowerStats : TowerStats
    {
        public Stat GAIN;
        public Stat LUK;

        public GatheringTowerStats(GatheringTowerStats other) : base(other)
        {
            GAIN = new Stat(other.GAIN);
            LUK = new Stat(other.LUK);
        }

        public int RandomisedGain
        {
            get
            {
                float randomResult = UnityEngine.Random.Range(0, 1);
                float luckThreshold = 1 / LUK.Final;
                float multiplier = randomResult > luckThreshold * 0.5f ?
                        (randomResult > luckThreshold? 2f : 1f) : 0.5f;

                return Mathf.RoundToInt(GAIN.Final * multiplier);
            }
        }

        public override void LevelUp()
        {
            base.LevelUp();
            GAIN.LevelUp();
            LUK.LevelUp();
        }
    }
}