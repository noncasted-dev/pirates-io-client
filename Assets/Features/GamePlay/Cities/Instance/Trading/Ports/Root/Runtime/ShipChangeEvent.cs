using GamePlay.Player.Entity.Components.Definition;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct ShipChangeEvent
    {
        public ShipChangeEvent(ShipType ship)
        {
            Ship = ship;
        }
        
        public readonly ShipType Ship;
    }
}