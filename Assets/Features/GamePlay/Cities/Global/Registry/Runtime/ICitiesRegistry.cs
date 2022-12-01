using GamePlay.Cities.Instance.Root.Runtime;

namespace GamePlay.Cities.Global.Registry.Runtime
{
    public interface ICitiesRegistry
    {
        ICity GetCity(CityDefinition definition);
        ICity GetCity(CityType type);
    }
}