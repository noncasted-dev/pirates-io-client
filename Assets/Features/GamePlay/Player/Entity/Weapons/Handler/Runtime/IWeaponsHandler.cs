using GamePlay.Player.Entity.Weapons.Cannon.Root;

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public interface IWeaponsHandler
    {
        ICanon Canon { get; }
    }
}