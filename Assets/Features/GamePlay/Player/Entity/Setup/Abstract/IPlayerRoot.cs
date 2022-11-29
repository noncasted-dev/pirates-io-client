using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow;
using UnityEngine;
using VContainer.Unity;

namespace GamePlay.Player.Entity.Setup.Abstract
{
    public interface IPlayerRoot
    {
        Transform Transform { get; }

        UniTask OnBootstrapped(IFlowHandler flowHandler, LifetimeScope parent);
        void Respawn();
    }
}