#region

using GamePlay.Cities.Instance.Spawning.Runtime;

#endregion

namespace GamePlay.Cities.Instance.Root.Runtime
{
    public interface ICity
    {
        CityDefinition Definition { get; }
        ICitySpawnPoints SpawnPoints { get; }
    }
}