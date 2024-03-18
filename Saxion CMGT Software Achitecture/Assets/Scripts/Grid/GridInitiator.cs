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
        [SerializeField]
        public TileBase EnemySpawnerTile;
        [SerializeField]
        public TileBase NexusTile;
        [Space(5)]
        [SerializeField]
        private GridCellShell _openShellPrefab;
        [SerializeField]
        private GridCellShell _enemySpawnerShellPrefab;
        [SerializeField]
        private GridCellShell _nexusShellPrefab;
        [Space(5)]
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

        private BoundsInt _bounds;
        private Vector2Int _boundsOffset;
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
           _bounds = _tilemap.cellBounds;
            _boundsOffset = new(-_bounds.xMin, -_bounds.yMin);

            _grid = new GridCell[_bounds.xMax + _boundsOffset.x, _bounds.yMax + _boundsOffset.y];

            OpenShellPass();
            FunctionalTilePass();

            FinalizeInitialisation(new()
            {
                Offset = _boundsOffset,
                CellSize = _cellSize,
                TilesParent = _tilesParent
            });
        }
        private void OpenShellPass()
        {
            int sample;
            Vector3Int samplePosition;
            Vector2Int XYWithOffset;

            HashSet<Vector2Int> uniqueShellPositions = new();

            for (int x = _bounds.xMin; x < _bounds.xMax; x++)
            for (int y = _bounds.yMin; y < _bounds.yMax; y++)
            {
                samplePosition = new(x, y, 0);
                TileBase tile = _tilemap.GetTile(samplePosition);

                if (!_roadTiles.Contains(tile) || tile == NexusTile || tile == EnemySpawnerTile)
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

                OverwriteCell(x + _boundsOffset.x, y + _boundsOffset.y, GridCell.Empty());

                void Sample(bool isX)
                {
                    XYWithOffset = new((isX ? sample : x) + _boundsOffset.x, (isX ? y : sample) + _boundsOffset.y);

                    if (GridCell.IsEmpty(_grid[XYWithOffset.x, XYWithOffset.y])
                    && sample < (isX ? _bounds.xMax : _bounds.yMax)
                    && sample >= (isX ? -_boundsOffset.x : -_boundsOffset.y)
                    && !_roadTiles.Contains(_tilemap.GetTile(samplePosition))
                    && _nonSolidTiles.Contains(_tilemap.GetTile(samplePosition)))
                    {
                        OverwriteCell(XYWithOffset.x, XYWithOffset.y, new GridCell(null));

                        if (!uniqueShellPositions.Contains(XYWithOffset))
                            uniqueShellPositions.Add(XYWithOffset);
                    }
                }
            }
            InstantiateShellBatch(uniqueShellPositions, _openShellPrefab);
        }
        private void FunctionalTilePass()
        {
            Vector3Int samplePosition;
            Vector2Int XYWithOffset;

            for (int x = _bounds.xMin; x < _bounds.xMax; x++)
            for (int y = _bounds.yMin; y < _bounds.yMax; y++)
            {
                samplePosition = new(x, y, 0);
                XYWithOffset = new Vector2Int(x + _boundsOffset.x, y + _boundsOffset.y);

                TileBase tile = _tilemap.GetTile(samplePosition);

                if (tile == NexusTile)
                    OverwriteCell(XYWithOffset.x, XYWithOffset.y, new GridCell(InstantiateShell(XYWithOffset, _nexusShellPrefab)));
                else if (tile == EnemySpawnerTile)
                    OverwriteCell(XYWithOffset.x, XYWithOffset.y, new GridCell(InstantiateShell(XYWithOffset, _enemySpawnerShellPrefab)));
            }
        }
        private void OverwriteCell(int x, int y, GridCell cell)
        {
            GridCell prevCell = _grid[x, y];
            if (!GridCell.IsEmpty(prevCell) && prevCell.BoundShell != cell.BoundShell)
                Destroy(prevCell.BoundShell);

            _grid[x, y] = cell;
        }
        private GridCellShell InstantiateShell(Vector2Int position, GridCellShell prefab)
        {
            GridCellShell instanced = Instantiate(prefab, _tilesParent);
            instanced.SetXY(position.x, position.y);
            instanced.transform.localPosition = new Vector3(position.x * _cellSize.x, position.y * _cellSize.y);
            return instanced;
        }
        private void InstantiateShellBatch(in HashSet<Vector2Int> positionsToInstantiate, GridCellShell prefab)
        {
            foreach (var position in positionsToInstantiate)
                InstantiateShell(position, prefab);

            positionsToInstantiate.Clear();
        }
        private void FinalizeInitialisation(ShellPositioningData data)
        {
            GridRegistrar.Refresh(_grid, data);
            Destroy(this);
        }
    }
}