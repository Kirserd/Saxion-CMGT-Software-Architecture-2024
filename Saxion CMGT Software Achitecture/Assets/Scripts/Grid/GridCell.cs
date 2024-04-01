using MIRAI.Grid.Cell;

namespace MIRAI.Grid
{
    public struct GridCell
    {
        public bool IsRoad;
        public bool IsLocked;
        public GridCellShell BoundShell;

        public GridCell(GridCellShell boundShell, bool isRoad = false)
        {
            IsRoad = isRoad;
            IsLocked = false;
            BoundShell = boundShell;
        }
        public GridCell(bool isOpened = true, bool isRoad = false)
        {
            IsRoad = isRoad;
            IsLocked = !isOpened; // smol trolling
            BoundShell = null;
        }
        public static GridCell Empty() => new();
        public static GridCell Road() => new(isRoad: true);

        public static bool IsEmpty(GridCell cell) => cell.BoundShell == null;
    }
}