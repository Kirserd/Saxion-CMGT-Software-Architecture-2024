using UnityEngine;

namespace MIRAI.Grid.Cell
{
    [CreateAssetMenu(fileName = "GatheringTowerBlueprint", menuName = "Static Data/Tower Blueprints/Gathering", order = 1)]
    public class GatheringTowerBlueprint : TowerBlueprint
    {
        [Header("--------------STATS--------------")]
        public GatheringTowerStats Stats;
    }
}