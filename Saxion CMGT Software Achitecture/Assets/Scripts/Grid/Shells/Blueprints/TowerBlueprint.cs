using System;
using UnityEngine;

namespace MIRAI.Grid.Cell
{
    public class TowerBlueprint : ScriptableObject
    {
        [Serializable]
        public class TargetingRules
        {
            public enum TargetingMode
            {
                Closest     = 0,
                LowestHP    = 1,
                AOE         = 2
            }
            public TargetingMode Mode;
            public string[] Tags;
        }

        [Header("--------------TOOLTIP------------")]
        public ShellTooltip Tooltip;
        [Header("--------------SPRITES------------")]
        public TowerSpriteSet SpriteSet;
        [Header("--------------RULES--------------")]
        public TargetingRules Rules;
    }
}