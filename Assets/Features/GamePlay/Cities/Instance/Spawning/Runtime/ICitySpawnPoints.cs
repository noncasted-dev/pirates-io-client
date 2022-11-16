#region

using UnityEngine;

#endregion

namespace GamePlay.Cities.Instance.Spawning.Runtime
{
    public interface ICitySpawnPoints
    {
        Vector2 GetRandom();
    }
}