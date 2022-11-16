#region

using System;
using GamePlay.Cities.Instance.Root.Runtime;

#endregion

namespace GamePlay.Cities.Global.Registry.Runtime
{
    [Serializable]
    public class FactionCitiesDictionary : SerializableDictionary<CityDefinition, CityRoot>
    {
    }
}