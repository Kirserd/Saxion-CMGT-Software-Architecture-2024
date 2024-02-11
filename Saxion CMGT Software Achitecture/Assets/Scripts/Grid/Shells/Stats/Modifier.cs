using System;

namespace MIRAI.Grid.Cell
{
    [Serializable]
    public struct Modifier
    {
        public ModifierType Type;
        public float Amount;

        public Modifier(ModifierType type, float amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}