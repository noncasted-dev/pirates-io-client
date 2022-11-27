using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.Level.Environment.Tools
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class FirstLayerBuilder : MonoBehaviour
    {
        [SerializeField] private RuleTile _groundTile;
        [SerializeField] private RuleTile _grassTopTile;
        [SerializeField] private RuleTile _outlineTile;
        [SerializeField] private RuleTile _grassDownMiddleTile;
        [SerializeField] private RuleTile _grassDownLeftTile;
        [SerializeField] private RuleTile _grassDownRightTile;
        [SerializeField] private RuleTile _sandTile;
        [SerializeField] private RuleTile _shallowTile;

        [SerializeField] private Tilemap _grassDown;
        [SerializeField] private Tilemap _grassTop;
        [SerializeField] private Tilemap _ground;
        [SerializeField] private Tilemap _outline;
        [SerializeField] private Tilemap _sand;
        [SerializeField] private Tilemap _shallow;

        [SerializeField] private GroundColorPalette _groundColors;
        [SerializeField] private GroundColorPalette _sandColors;

        private bool _isGenerating;

#if UNITY_EDITOR
        private void OnEnable()
        {
            Tilemap.tilemapTileChanged += TilemapChanged;
        }

        private void OnDisable()
        {
            Tilemap.tilemapTileChanged -= TilemapChanged;
        }

        [Button("Generate")]
        public void Generate()
        {
            ProcessGenerate().Forget();
        }

        public async UniTask ProcessGenerate()
        {
            _isGenerating = true;

            var groundColors = _groundColors.GetColors();
            var sandColors = _sandColors.GetColors();

            _grassTop.ClearAllTiles();
            _grassDown.ClearAllTiles();
            _outline.ClearAllTiles();

            _ground.ResizeBounds();
            _grassTop.ResizeBounds();
            _outline.ResizeBounds();
            _sand.ResizeBounds();

            var queuedSand = new List<Vector3Int>();

            foreach (var tilePosition in _sand.cellBounds.allPositionsWithin)
            {
                if (_sand.HasTile(tilePosition) == false)
                    continue;

                _outline.SetTile(tilePosition, _outlineTile);

                var sandColor = sandColors[Random.Range(0, 3)];
                SetColor(_sand, tilePosition, sandColor);

                var upPosition = tilePosition;
                upPosition.y += 1;

                if (_ground.HasTile(upPosition) == true)
                    queuedSand.Add(upPosition);
                
                var downPosition = tilePosition;
                downPosition.y -= 1;

                if (_ground.HasTile(downPosition) == true)
                    queuedSand.Add(downPosition);

                var left = new Vector3Int(tilePosition.x - 1, tilePosition.y);
                var right = new Vector3Int(tilePosition.x + 1, tilePosition.y);
                var up = new Vector3Int(tilePosition.x, tilePosition.y + 1);
                var down = new Vector3Int(tilePosition.x, tilePosition.y - 1);

                if (_ground.HasTile(left) == false)
                {
                    _shallow.SetTile(left, _shallowTile);
                    _shallow.SetTile(new Vector3Int(left.x, left.y + 1), _shallowTile);
                    _shallow.SetTile(new Vector3Int(left.x, left.y - 1), _shallowTile);
                }

                if (_ground.HasTile(right) == false)
                {
                    _shallow.SetTile(right, _shallowTile);
                    _shallow.SetTile(new Vector3Int(right.x, right.y + 1), _shallowTile);
                    _shallow.SetTile(new Vector3Int(right.x, right.y - 1), _shallowTile);
                }

                if (_ground.HasTile(up) == false)
                    _shallow.SetTile(up, _shallowTile);

                if (_ground.HasTile(down) == false)
                    _shallow.SetTile(down, _shallowTile);
            }

            foreach (var sand in queuedSand)
            {
                _sand.SetTile(sand, _sandTile);
                var newColor = sandColors[Random.Range(0, 3)];
                SetColor(_sand, sand, newColor);
            }

            foreach (var tilePosition in _ground.cellBounds.allPositionsWithin)
            {
                if (_ground.HasTile(tilePosition) == false)
                    continue;

                var downPosition = tilePosition;
                downPosition.y -= 1;

                if (_ground.HasTile(downPosition) == false)
                {
                    var downLeft = tilePosition;
                    downLeft.x -= 1;

                    var downRight = tilePosition;
                    downRight.x += 1;

                    var hasLeft = _ground.HasTile(downLeft);
                    var hasRight = _ground.HasTile(downRight);

                    if (hasLeft == true && hasRight == true)
                        _grassDown.SetTile(tilePosition, _grassDownMiddleTile);
                    else if (hasLeft == true)
                        _grassDown.SetTile(tilePosition, _grassDownRightTile);
                    else
                        _grassDown.SetTile(tilePosition, _grassDownLeftTile);
                }

                var left = new Vector3Int(tilePosition.x - 1, tilePosition.y);
                var right = new Vector3Int(tilePosition.x + 1, tilePosition.y);
                var up = new Vector3Int(tilePosition.x, tilePosition.y + 1);
                var down = new Vector3Int(tilePosition.x, tilePosition.y - 1);

                if (_ground.HasTile(left) == false)
                {
                    _shallow.SetTile(left, _shallowTile);
                    _shallow.SetTile(new Vector3Int(left.x, left.y + 1), _shallowTile);
                    _shallow.SetTile(new Vector3Int(left.x, left.y - 1), _shallowTile);
                }

                if (_ground.HasTile(right) == false)
                {
                    _shallow.SetTile(right, _shallowTile);
                    _shallow.SetTile(new Vector3Int(right.x, right.y + 1), _shallowTile);
                    _shallow.SetTile(new Vector3Int(right.x, right.y - 1), _shallowTile);
                }

                if (_ground.HasTile(up) == false)
                    _shallow.SetTile(up, _shallowTile);

                if (_ground.HasTile(down) == false)
                    _shallow.SetTile(down, _shallowTile);

                _shallow.SetTile(right, _shallowTile);
                _shallow.SetTile(up, _shallowTile);
                _shallow.SetTile(down, _shallowTile);
                _shallow.SetTile(tilePosition, _shallowTile);

                _ground.SetTile(tilePosition, _groundTile);
                _grassTop.SetTile(tilePosition, _grassTopTile);
                _outline.SetTile(tilePosition, _outlineTile);

                var color = groundColors[Random.Range(0, 3)];

                SetColor(_grassTop, tilePosition, color);
                SetColor(_ground, tilePosition, color);
            }

            _isGenerating = false;
        }

        [Button("Clear")]
        private void Clear()
        {
            _ground.ClearAllTiles();
            _grassTop.ClearAllTiles();
            _grassDown.ClearAllTiles();
            _sand.ClearAllTiles();
        }

        private void SetColor(Tilemap tilemap, Vector3Int position, Color color)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, color);
        }

        private void TilemapChanged(Tilemap tilemap, Tilemap.SyncTile[] tiles)
        {
            if (_isGenerating == true)
                return;

            if (tilemap != _ground)
                return;

            Tilemap.tilemapTileChanged -= TilemapChanged;

            foreach (var tile in tiles)
                if (_ground.HasTile(tile.position) == true)
                {
                    var colors = _groundColors.GetColors();
                    var color = colors[Random.Range(0, 3)];

                    _grassTop.SetTile(tile.position, _grassTopTile);

                    SetColor(_grassTop, tile.position, color);
                    SetColor(_ground, tile.position, color);
                }
                else
                {
                    _grassTop.SetTile(tile.position, null);
                    _grassDown.SetTile(tile.position, null);
                }

            Tilemap.tilemapTileChanged += TilemapChanged;
        }
#endif
    }
}