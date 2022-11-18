using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.Level.Environment.Tools
{
    public class TileMapTool : MonoBehaviour
    {
        [Button("Clear")]
        private void Clear()
        {
            var tileMap = GetComponent<Tilemap>();
            
            tileMap.ClearAllTiles();
        }
    }
}