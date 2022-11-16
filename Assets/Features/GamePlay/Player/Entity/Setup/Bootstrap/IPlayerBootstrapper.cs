using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Setup.Bootstrap
{
    public interface IPlayerBootstrapper
    {
        UniTask Bootstrap(LifetimeScope parent);
    }
}