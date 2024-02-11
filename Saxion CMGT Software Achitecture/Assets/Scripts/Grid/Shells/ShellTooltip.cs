using UnityEngine;

namespace MIRAI.Grid
{
    [CreateAssetMenu(fileName = "CellDisplayData", menuName = "Static Data/CellDisplayData", order = 1)]
    public class ShellTooltip : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
    }
}