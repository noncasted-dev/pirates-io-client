using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.Level.Environment.Chunks.Tools
{
    [DisallowMultipleComponent]
    public class TreesGenerator : MonoBehaviour
    {
        [SerializeField] private TileBase _trees;

        [SerializeField] private Tilemap _groundTileMap;
        [SerializeField] private Tilemap _treesTileMap;

        [Button("Generate")]
        private void Generate()
        {
            _treesTileMap.ClearAllTiles();

            _groundTileMap.CompressBounds();

            foreach (var position in _groundTileMap.cellBounds.allPositionsWithin)
            {
                if (_groundTileMap.HasTile(position) == false)
                    continue;

                var world = _groundTileMap.CellToWorld(position);

                var start = new Vector3(world.x - 0.5f, world.y - 0.5f);

                var x = Random.Range(0, 10);
                var y = Random.Range(0, 10);
                
                var rawPosition = start + new Vector3(x, y, 0f) * 0.1f;
                var perlin = Perlin.perlin(rawPosition.x, rawPosition.y, 0);

                if (perlin < 0.5f)
                    continue;

                var treePosition = _treesTileMap.WorldToCell(rawPosition);

                _treesTileMap.SetTile(treePosition, _trees);
            }
        }
    }
}