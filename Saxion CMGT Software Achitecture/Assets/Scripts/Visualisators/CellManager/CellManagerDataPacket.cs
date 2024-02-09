using MIRAI.Grid;
using MIRAI.Grid.Cell;

public struct CellManagerDataPacket 
{ 
    public enum Screen
    {
        Upgrade = 0,
        Build = 1
    }

    public Screen Current;
    public readonly GridCellShell Selection => GridInteractor.Selection;
}
