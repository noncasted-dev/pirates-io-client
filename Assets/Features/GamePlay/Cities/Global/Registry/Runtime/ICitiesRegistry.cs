#region

using GamePlay.Cities.Instance.Root.Runtime;

#endregion

namespace GamePlay.Cities.Global.Registry.Runtime
{
    public interface ICitiesRegistry
    {
        ICity GetCity(CityDefinition definition);
    }
}