using UnityEngine;

namespace GamePlay.Cities.Instance.Spawning.Runtime
{
    public class CitySpawnPoints : MonoBehaviour, ICitySpawnPoints
    {
        [SerializeField] private Transform[] _spawnPoints;

        public Vector2 GetRandom()
        {
            if (_spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points assigned");
                return transform.position;
            }

            var randomIndex = Random.Range(0, _spawnPoints.Length);

            return _spawnPoints[randomIndex].position;
        }
    }
}