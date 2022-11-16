using UnityEngine;

namespace GamePlay.Services.PlayerSpawn.SpawnPoints
{
    public class PlayerSpawnPoints : MonoBehaviour, ISpawnPoints
    {
        [SerializeField] private Transform _spawnPoint;

        public Vector2 GetSpawnPoint()
        {
            return _spawnPoint.position;
        }
    }
}