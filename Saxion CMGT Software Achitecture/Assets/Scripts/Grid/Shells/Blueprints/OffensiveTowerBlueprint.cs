using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [CreateAssetMenu(fileName = "OffensiveTowerBlueprint", menuName = "Static Data/Tower Blueprints/Offensive", order = 0)]
    public class OffensiveTowerBlueprint : TowerBlueprint
    {
        [Header("--------------STATS--------------")]
        public OffensiveTowerStats Stats;
    }
}