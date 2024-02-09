using MIRAI.Grid.Cell;

namespace MIRAI.Grid
{
    public struct GridCell
    {
        public bool IsLocked;
        public GridCellShell BoundShell;

        public GridCell(GridCellShell boundShell)
        {
            IsLocked = false;
            BoundShell = boundShell;
        }
        public GridCell(bool isOpened = true)
        {
            IsLocked = !isOpened; // smol trolling
            BoundShell = null;
        }
        public static GridCell Empty() => new();
        public static bool IsEmpty(GridCell cell) => cell.BoundShell == null;
    }
}