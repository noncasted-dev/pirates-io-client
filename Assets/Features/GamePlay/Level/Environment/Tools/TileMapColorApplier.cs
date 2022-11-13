using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.Level.Environment.Tools
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class TileMapColorApplier : MonoBehaviour
    {
        [SerializeField] private Tilemap _ground;
        [SerializeField] private Tilemap _grassTop;
        [SerializeField] private Tilemap _grassDown;

        [SerializeField] private RuleTile _grassTile;

        [SerializeField] private GroundColorPalette _palette;

        private bool _isGenerating;

        #if UNITY_EDITOR
        private void OnEnable()
        {
            Tilemap.tilemapTileChanged += TilemapChanged;
        }

        [Button("Generate")]
        private void Generate()
        {
            Tilemap.tilemapTileChanged -= TilemapChanged;
            _isGenerating = true;

            var colors = _palette.GetColors();

            _grassTop.ClearAllTiles();

            _ground.ResizeBounds();
            _grassTop.ResizeBounds();

            foreach (var tilePosition in _ground.cellBounds.allPositionsWithin)
            {
                var position = _ground.CellToWorld(tilePosition);

                var color = colors[Random.Range(0, 3)];

                if (_ground.HasTile(tilePosition) == false)
                    continue;

                _grassTop.SetTile(tilePosition, _grassTile);

                SetColor(_grassTop, position, color);
                SetColor(_ground, position, color);
            }

            Tilemap.tilemapTileChanged += TilemapChanged;
            _isGenerating = false;
        }

        [Button("Clear")]
        private void Clear()
        {
            _ground.ClearAllTiles();
            _grassTop.ClearAllTiles();
            _grassDown.ClearAllTiles();
        }

        private void SetColor(Tilemap tilemap, Vector3 position, Color color)
        {
            var localPosition = tilemap.WorldToCell(position);

            tilemap.SetTileFlags(localPosition, TileFlags.None);
            tilemap.SetColor(localPosition, color);
        }

        private void TilemapChanged(Tilemap tilemap, Tilemap.SyncTile[] tiles)
        {
            if (_isGenerating == true)
                return;

            if (tilemap != _ground)
                return;

            foreach (var tile in tiles)
                if (_ground.HasTile(tile.position) == true)
                {
                    var colors = _palette.GetColors();
                    var position = _ground.CellToWorld(tile.position);
                    var color = colors[Random.Range(0, 3)];

                    _grassTop.SetTile(tile.position, _grassTile);

                    SetColor(_grassTop, position, color);
                    SetColor(_ground, position, color);
                }
                else
                {
                    _grassTop.SetTile(tile.position, null);
                    _grassDown.SetTile(tile.position, null);
                }
        }
#endif

    }
}