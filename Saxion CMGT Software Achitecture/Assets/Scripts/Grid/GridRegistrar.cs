using MIRAI.Grid.Cell;
using UnityEngine;

namespace MIRAI.Grid
{
    public struct ShellPositioningData
    {
        public Vector2Int Offset;
        public Vector2 CellSize;
        public Transform TilesParent;
    }
    public static class GridRegistrar
    {
        private static ShellPositioningData _instantiationData;
        private static GridCell[,] _grid;
        public static void Refresh(GridCell[,] grid, ShellPositioningData instantiationData)
        {
            _grid = grid;
            _instantiationData = instantiationData;
        }
        public static bool ValidateAt(int x, int y) => IsWithinBounds(x, y) && IsEmptyAt(x, y);
        public static bool IsEmptyAt(int x, int y) => GridCell.IsEmpty(_grid[x, y]);
        public static bool IsWithinBounds(int x, int y) => x < _grid.GetLength(0) && y < _grid.GetLength(1);

        public static bool TryRegisterAt(int x, int y, GridCellShell shell)
        {
            if (!ValidateAt(x, y))
                return false;

            _grid[x, y] = new GridCell(shell);

            TryReposition(shell, x, y);
            return true;
        }
        public static bool OverwriteAt(int x, int y, GridCellShell shell)
        {
            if (!IsWithinBounds(x, y))
                return false;

            if (!IsEmptyAt(x, y))
                _grid[x, y].BoundShell.DestroySelf();

            _grid[x, y] = new GridCell(shell);

            TryReposition(shell, x, y);
            return true;
        }

        public static GridCell GetAt(int x, int y) => IsWithinBounds(x, y) ? _grid[x, y] : GridCell.Empty();
        
        private static void TryReposition(GridCellShell shell, int x, int y)
        {
            if (shell is null)
                return;

            shell.SetXY(x, y);

            shell.transform.SetParent(_instantiationData.TilesParent);
            shell.transform.localPosition = new Vector3
            (
                x * _instantiationData.CellSize.x, 
                y * _instantiationData.CellSize.y
            );
        }
    }
}