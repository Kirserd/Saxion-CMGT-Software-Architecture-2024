using System;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class OffensiveTowerStats : TowerStats
    {
        public enum DamageType
        {
            Pierce = 0,
            Slash = 1,
            Impact = 2
        }

        public Stat DMG;
        public DamageType TYPE;

        public OffensiveTowerStats(OffensiveTowerStats other) : base(other)
        {
            DMG = new Stat(other.DMG);
            TYPE = other.TYPE;
        }

        public override void LevelUp()
        {
            base.LevelUp();
            DMG.LevelUp();
        }
    }
}