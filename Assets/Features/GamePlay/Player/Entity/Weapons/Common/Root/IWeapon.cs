#region

using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using VContainer.Unity;

#endregion

namespace GamePlay.Player.Entity.Weapons.Common.Root
{
    public interface IWeapon
    {
        string Name { get; }

        UniTask OnBootstrapped(IFlowHandler flowHandler, LifetimeScope parent);
    }
}