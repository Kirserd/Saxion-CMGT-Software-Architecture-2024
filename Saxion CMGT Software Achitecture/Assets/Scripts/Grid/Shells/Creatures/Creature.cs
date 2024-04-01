using MIRAI.Grid.Cell;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private CreatureStats _stats;

    public int X { get; protected set; }
    public int Y { get; protected set; }

    public void SetXY(int x, int y)
    {
        X = x;
        Y = y;
    }
}