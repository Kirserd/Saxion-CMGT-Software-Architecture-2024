using MIRAI.Grid.Cell;

namespace MIRAI.Grid
{
    public static class GridRegistrar
    {
        private static GridCell[,] _grid;
        public static void Refresh(GridCell[,] grid) => _grid = grid;

        public static bool ValidateAt(int x, int y) => IsWithinBounds(x, y) && IsEmptyAt(x, y);
        public static bool IsEmptyAt(int x, int y) => GridCell.IsEmpty(_grid[x, y]);
        public static bool IsWithinBounds(int x, int y) => x < _grid.GetLength(0) && y < _grid.GetLength(1);

        public static bool TryRegisterAt(int x, int y, GridCellShell shell)
        {
            if (!ValidateAt(x, y))
                return false;

            _grid[x, y] = new GridCell(shell);
            return true;
        }
        public static bool OverwriteAt(int x, int y, GridCellShell shell)
        {
            if (!IsWithinBounds(x, y))
                return false;

            if (!IsEmptyAt(x, y))
                _grid[x, y].BoundShell.DestroySelf();

            _grid[x, y] = new GridCell(shell);
            return true;
        }
        public static GridCell GetAt(int x, int y) => IsWithinBounds(x, y) ? _grid[x, y] : GridCell.Empty();
    }
}