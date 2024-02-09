using UnityEngine;

namespace MIRAI.Grid
{
    [CreateAssetMenu(fileName = "CellDisplayData", menuName = "Static Data/CellDisplayData", order = 1)]
    public class CellDisplayData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
    }

    [CreateAssetMenu(fileName = "TowerModelData", menuName = "Static Data/TowerModelData", order = 2)]
    public class TowerModelData : ScriptableObject
    {   

    }
}