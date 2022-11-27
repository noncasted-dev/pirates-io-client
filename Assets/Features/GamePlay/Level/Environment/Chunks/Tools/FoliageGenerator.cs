using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.Level.Environment.Chunks.Tools
{
    [DisallowMultipleComponent]
    public class FoliageGenerator : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _stonesPercent = 0.3f;
        [SerializeField] [Min(0f)] private float _propsAmount = 0.5f;
        
        [SerializeField] private TileBase _trees;
        [SerializeField] private TileBase _stones;

        [SerializeField] private Tilemap _groundTileMap;
        [SerializeField] private Tilemap _propsTileMap;

        [Button("Generate")]
        private void Generate()
        {
            _propsTileMap.ClearAllTiles();

            _groundTileMap.CompressBounds();

            foreach (var position in _groundTileMap.cellBounds.allPositionsWithin)
            {
                if (_groundTileMap.HasTile(position) == false)
                    continue;

                var down = new Vector3Int(position.x, position.y - 1, 0);

                if (_groundTileMap.HasTile(down) == false)
                    continue;
                
                var up = new Vector3Int(position.x, position.y + 1, 0);

                if (_groundTileMap.HasTile(up) == false)
                    continue;
                
                var left = new Vector3Int(position.x - 1, position.y, 0);

                if (_groundTileMap.HasTile(left) == false)
                    continue;
                
                var right = new Vector3Int(position.x + 1, position.y, 0);

                if (_groundTileMap.HasTile(right) == false)
                    continue;
                
                var world = _groundTileMap.CellToWorld(position);

                var start = new Vector3(world.x - 0.5f, world.y - 0.5f);

                var x = Random.Range(0, 10);
                var y = Random.Range(0, 10);
                
                var rawPosition = start + new Vector3(x, y, 0f) * 0.1f;
                var perlin = Perlin.perlin(rawPosition.x, rawPosition.y, 0);

                if (perlin < _propsAmount)
                    continue;
                
                TileBase tile;

                var random = Random.Range(0f, 1f);

                if (random < _stonesPercent)
                    tile = _stones;
                else
                    tile = _trees;
                
                var treePosition = _propsTileMap.WorldToCell(rawPosition);

                _propsTileMap.SetTile(treePosition, tile);
            }
        }
    }
}