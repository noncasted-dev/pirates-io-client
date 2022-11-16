#region

using Cysharp.Threading.Tasks;
using VContainer.Unity;

#endregion

namespace GamePlay.Player.Entity.Setup.Bootstrap
{
    public interface IPlayerBootstrapper
    {
        UniTask Bootstrap(LifetimeScope parent);
    }
}