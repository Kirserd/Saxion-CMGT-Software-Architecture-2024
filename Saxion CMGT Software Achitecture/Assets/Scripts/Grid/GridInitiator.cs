using MIRAI.Grid.Cell;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MIRAI.Grid
{
    [RequireComponent(typeof(Tilemap))]
    public class GridInitiator : MonoBehaviour
    {
        #region PARAMETER FIELDS

        [Header("Parameters\n-------------")]
        [SerializeField]
        public TileBase[] RoadTiles;
        [SerializeField]
        public TileBase[] NonSolidTiles;
        [Space(5)]
        [SerializeField]
        private GridCellShell _openShellPrefab;
        [SerializeField]
        private Transform _tilesParent;

        #endregion

        #region OPERATIONAL FIELDS

        public static GridInitiator Instance;

        private readonly static HashSet<TileBase> _roadTiles = new();
        private readonly static HashSet<TileBase> _nonSolidTiles = new();

        private GridCell[,] _grid;

        private Tilemap _tilemap;
        private Vector2 _cellSize;
        #endregion

        private void OnValidate()
        {
            foreach (var tile in RoadTiles)
                _roadTiles.Add(tile);
            foreach (var tile in NonSolidTiles)
                _nonSolidTiles.Add(tile);
        }

        private void Awake() => Instance = this;
        private void Start()
        {
            _tilemap = GetComponent<Tilemap>();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _cellSize = _tilemap.cellSize;
            BoundsInt bounds = _tilemap.cellBounds;
            Vector2Int boundsOffset = new(-bounds.xMin, -bounds.yMin);

            _grid = new GridCell[bounds.xMax + boundsOffset.x, bounds.yMax + boundsOffset.y];

            int sample;
            Vector3Int samplePosition;
            Vector2Int XYWithOffset;

            HashSet<Vector2Int> uniqueShellPositions = new();

            ShellPositioningData shellInstantiationData = new()
            {
                Offset = boundsOffset,
                CellSize = _cellSize,
                TilesParent = _tilesParent
            };

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                samplePosition = new(x, y, 0);
                TileBase tile = _tilemap.GetTile(samplePosition);

                if (!_roadTiles.Contains(tile))
                    continue;

                sample = x + 1;
                samplePosition = new(sample, y, 0);
                Sample(true);

                sample = x - 1;
                samplePosition = new(sample, y, 0);
                Sample(true);

                sample = y + 1;
                samplePosition = new(x, sample, 0);
                Sample(false);

                sample = y - 1;
                samplePosition = new(x, sample, 0);
                Sample(false);

                OverwriteCell(x + boundsOffset.x, y + boundsOffset.y, GridCell.Empty());

                void Sample(bool isX)
                {
                    XYWithOffset = new((isX ? sample : x) + boundsOffset.x, (isX ? y : sample) + boundsOffset.y);

                    if (GridCell.IsEmpty(_grid[XYWithOffset.x, XYWithOffset.y])
                    && sample < (isX ? bounds.xMax : bounds.yMax)
                    && sample >= (isX ? -boundsOffset.x : -boundsOffset.y)
                    && !_roadTiles.Contains(_tilemap.GetTile(samplePosition))
                    && _nonSolidTiles.Contains(_tilemap.GetTile(samplePosition)))
                    {
                        OverwriteCell(XYWithOffset.x, XYWithOffset.y, new GridCell(null));

                        if(!uniqueShellPositions.Contains(XYWithOffset))
                            uniqueShellPositions.Add(XYWithOffset);
                    }
                }
            }
            InstantiateShellBatch(uniqueShellPositions);
            FinalizeInitialisation(shellInstantiationData);
        }
        private void OverwriteCell(int x, int y, GridCell cell)
        {
            GridCell prevCell = _grid[x, y];
            if (!GridCell.IsEmpty(prevCell) && prevCell.BoundShell != cell.BoundShell)
                Destroy(prevCell.BoundShell);

            _grid[x, y] = cell;
        }
        private void InstantiateShellBatch(in HashSet<Vector2Int> positionsToInstantiate)
        {
            foreach (var position in positionsToInstantiate)
            {
                GridCellShell instanced = Instantiate(_openShellPrefab, _tilesParent);
                instanced.SetXY(position.x, position.y);
                instanced.transform.localPosition = new Vector3(position.x * _cellSize.x, position.y * _cellSize.y);
            }
            positionsToInstantiate.Clear();
        }
        private void FinalizeInitialisation(ShellPositioningData data)
        {
            GridRegistrar.Refresh(_grid, data);
            Destroy(this);
        }
    }
}