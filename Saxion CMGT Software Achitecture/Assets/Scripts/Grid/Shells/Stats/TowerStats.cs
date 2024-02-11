using System;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class TowerStats : GeneralStats
    {
        public int Level { get; private set; } = -1;
        public int MaxLevel = 1;

        public Stat SPD;
        public Stat RNG;

        private float _actDelayCounter = 0;
        private float _actDelayTarget => (4f / (SPD.Final + 4f));

        public TowerStats(TowerStats other) : base(other)
        {
            Level = other.Level;
            MaxLevel = other.MaxLevel;
            SPD = new Stat(other.SPD);
            RNG = new Stat(other.RNG);
            _actDelayCounter = 0;
        }

        public bool IsReadyToAct()
        {
            _actDelayCounter++;
            if (_actDelayCounter < _actDelayTarget)
                return false;

            _actDelayCounter = 0;
            return true;
        }

        public virtual void LevelUp()
        {
            if (Level >= MaxLevel)
                return;

            Level++;
            SPD.LevelUp();
            RNG.LevelUp();

            HP = MaxHP;
        }
    }
}