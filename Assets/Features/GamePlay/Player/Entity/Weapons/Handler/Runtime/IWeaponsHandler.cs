#region

using GamePlay.Player.Entity.Weapons.Cannon.Root;

#endregion

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public interface IWeaponsHandler
    {
        ICanon Canon { get; }
    }
}