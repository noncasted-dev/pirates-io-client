using UnityEngine;

namespace GamePlay.Player.Entity.Components.ShipResources.Runtime
{
    public readonly struct ResourcesChangedEvent
    {
        public ResourcesChangedEvent(IShipResources resources)
        {
            Resources = resources;
        }
        
        public readonly IShipResources Resources;
    }
}