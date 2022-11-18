using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace GamePlay.Level.Environment.Tools
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class SecondLayerBuilder : MonoBehaviour
    {
        [SerializeField] private RuleTile _groundTile;
        [SerializeField] private RuleTile _grassTopTile;
        [SerializeField] private RuleTile _outlineTile;
        [SerializeField] private RuleTile _grassDownMiddleTile;
        [SerializeField] private RuleTile _cornerTile;

        [SerializeField] private Tilemap _grassDown;
        [SerializeField] private Tilemap _grassTop;
        [SerializeField] private Tilemap _ground;
        [SerializeField] private Tilemap _outline;
        [SerializeField] private Tilemap _halfOutline;
        [SerializeField] private Tilemap _corner;

        [SerializeField] private GroundColorPalette _palette;

        [SerializeField] private bool _isEditing;

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
        private void Generate()
        {
            ProcessGenerate().Forget();
        }

        private async UniTaskVoid ProcessGenerate()
        {
            _isGenerating = true;

            var colors = _palette.GetColors();

            _grassTop.ClearAllTiles();
            _grassDown.ClearAllTiles();
            _outline.ClearAllTiles();
            _halfOutline.ClearAllTiles();
            _corner.ClearAllTiles();

            _ground.ResizeBounds();
            _grassTop.ResizeBounds();
            _outline.ResizeBounds();

            foreach (var tilePosition in _ground.cellBounds.allPositionsWithin)
            {
                var color = colors[Random.Range(0, 3)];

                if (_ground.HasTile(tilePosition) == false)
                    continue;

                var downPosition = tilePosition;
                downPosition.y -= 1;

                if (_ground.HasTile(downPosition) == false)
                {
                    _corner.SetTile(downPosition, _cornerTile);
                    _grassDown.SetTile(tilePosition, _grassDownMiddleTile);
                }

                _ground.SetTile(tilePosition, _groundTile);
                _grassTop.SetTile(tilePosition, _grassTopTile);
                _outline.SetTile(tilePosition, _outlineTile);
                _halfOutline.SetTile(tilePosition, _outlineTile);

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
            _outline.ClearAllTiles();
            _halfOutline.ClearAllTiles();
            _corner.ClearAllTiles();
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
            
            if (_isEditing == false)
                return;

            if (tilemap != _ground)
                return;
            
            Tilemap.tilemapTileChanged -= TilemapChanged;

            foreach (var tile in tiles)
            {
                if (_ground.HasTile(tile.position) == true)
                {
                    var colors = _palette.GetColors();
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
            }
            
            Tilemap.tilemapTileChanged += TilemapChanged;
        }
#endif
    }
}