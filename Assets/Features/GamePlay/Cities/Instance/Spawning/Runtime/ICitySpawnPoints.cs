using UnityEngine;

namespace GamePlay.Cities.Instance.Spawning.Runtime
{
    public interface ICitySpawnPoints
    {
        Vector2 GetRandom();
    }
}