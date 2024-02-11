using System;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public class GeneralStats
    {
        public Stat VIT;

        public int HP;

        public GeneralStats(GeneralStats other)
        {
            VIT = new Stat(other.VIT);
            HP = other.HP;
        }

        public int MaxHP => Mathf.RoundToInt(VIT.Final * 24 + VIT.Final * (VIT.Final * 0.4f));
        public int MinHP => 0;
    }
}