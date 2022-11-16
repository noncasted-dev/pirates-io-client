#region

using System;
using Common.ReadOnlyDictionaries.Runtime;
using GamePlay.Factions.Common;

#endregion

namespace GamePlay.Cities.Global.Registry.Runtime
{
    [Serializable]
    public class CitiesDictionary : ReadOnlyDictionary<FactionType, FactionCitiesDictionary>
    {
    }
}