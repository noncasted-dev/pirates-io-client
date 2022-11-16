#region

using UnityEngine;

#endregion

namespace GamePlay.Services.PlayerSpawn.SpawnPoints
{
    public interface ISpawnPoints
    {
        Vector2 GetSpawnPoint();
    }
}