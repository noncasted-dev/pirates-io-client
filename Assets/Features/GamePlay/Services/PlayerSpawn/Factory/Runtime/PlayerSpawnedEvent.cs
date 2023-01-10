using GamePlay.Player.Entity.Components.Definition;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public class PlayerSpawnedEvent
    {
        public PlayerSpawnedEvent(ShipType shipType)
        {
            ShipType = shipType;
        }
        
        public readonly ShipType ShipType;
    }
}