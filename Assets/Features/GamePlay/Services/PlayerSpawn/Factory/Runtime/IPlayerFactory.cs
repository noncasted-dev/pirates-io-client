using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Root;

namespace GamePlay.Services.PlayerSpawn.Factory.Runtime
{
    public interface IPlayerFactory
    {
        UniTask<IPlayerRoot> Create();
    }
}