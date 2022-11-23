using GamePlay.Player.Entity.Components.Definition;

namespace GamePlay.Services.LevelLoops.Runtime
{
    public interface ILevelLoop
    {
        void Respawn(ShipType ship);
    }
}