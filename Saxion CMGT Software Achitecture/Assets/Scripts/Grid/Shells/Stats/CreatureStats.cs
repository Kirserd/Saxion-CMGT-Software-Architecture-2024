using System;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class CreatureStats : GeneralStats
    {
        public CreatureStats(GeneralStats other) : base(other)
        {
        }
    }
}