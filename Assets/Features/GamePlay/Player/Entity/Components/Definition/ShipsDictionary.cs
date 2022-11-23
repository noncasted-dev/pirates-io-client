using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace GamePlay.Player.Entity.Components.Definition
{
    [Serializable]
    public class ShipsDictionary : ReadOnlyDictionary<ShipType, TradableShipConfig>
    {
        
    }
}